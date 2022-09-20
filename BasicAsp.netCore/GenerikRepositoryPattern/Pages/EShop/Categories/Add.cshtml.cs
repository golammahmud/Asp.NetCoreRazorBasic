using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GenerikRepositoryPattern.Pages.EShop.Categories
{
    public class AddModel : PageModel
    {

        private IGenerik<Category> _Category;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AddModel(IGenerik<Category> category, IWebHostEnvironment webHostEnvironment)
        {
            this._Category = category;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public CategoryViewModel Category { get; set; }
        public void OnGet(int? id)
        {
            Category = new CategoryViewModel();
            if (id.HasValue)
            {

                var DomainCategory = _Category.GetById(id.Value);
                {
                    Category.Id = DomainCategory.Id;
                    Category.Name = DomainCategory.Name;
                    //Category.Products = DomainCategory.Products.ToList();
                    Category.FileName = DomainCategory.FileName;
                    Category.FileData = DomainCategory.FileData;
                    Category.CreatedAt = DomainCategory.CreatedAt;

                }
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid != true)
            {
                return Page();
            }
            if (Category.Id > 0)
            {
                var UpdateCategory = _Category.GetById(Category.Id); 
                {
                    UpdateCategory.Name = Category.Name;
                    UpdateCategory.CreatedAt = Category.CreatedAt;
                };
                //uploads file to folder
                if (Category.FileData == null || Category.FormFile != null)
                {
                   
                    if (IsFileValid(Category.FormFile))
                    {
                        if (UpdateCategory.FileName != null)
                        {
                            string uploadedFile = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads/Category", UpdateCategory.FileName);
                            System.IO.File.Delete(uploadedFile);
                        }
                      
                        if (Category.FormFile.Length > 2097152)
                        {
                            UpdateCategory.FileName = ProcessUploadedFile(Category.FormFile);
                        }
                        UpdateCategory.FileData = GetFileBytes(Category.FormFile);
                        //UpdateCategory.FileName = Category.FormFile.FileName;
                    }
                    else
                    {
                       
                        ModelState.AddModelError("Collection Document", "No Document Uploaded");
                    }  
                }
               
                _Category.Update(UpdateCategory);
                _Category.Save();
                return RedirectToPage("Index");

            }
            else
            {
                Category NewAddCategory = new Category();
                {
                    NewAddCategory.Name = Category.Name;
                    NewAddCategory.CreatedAt = Category.CreatedAt;
                };


                //uploads file to folder
                if (Category.FormFile.Length > 0)
                {
                    if (IsFileValid(Category.FormFile))
                    {
                        if (Category.FormFile.Length > 2097152)
                        {
                            //string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads/Category");
                            //string uniqueFileName = Guid.NewGuid().ToString() + "-" + Category.FormFile.FileName;
                            //string filePath = Path.Combine(uploadFolder, uniqueFileName);
                            //using (var stream = new FileStream(filePath, FileMode.Create))
                            //{
                            //    Category.FormFile.CopyToAsync(stream);
                            //    NewAddCategory.FileName = uniqueFileName;
                            //}
                            NewAddCategory.FileName = ProcessUploadedFile(Category.FormFile);
                        }
                        //uploads file to database
                        //if (IsFileValid(Category.FormFile))
                        //{
                        NewAddCategory.FileData = GetFileBytes(Category.FormFile);
                        NewAddCategory.FileName = Category.FormFile.FileName;
                        //CollectionData.FileType = document.ContentType;
                    }
                    else
                    {
                        ModelState.AddModelError("Collection Document", "No Document Uploaded");
                    }
                    //or
                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    await Category.FormFile.CopyToAsync(memoryStream);
                    //    if (memoryStream.Length < 2097152)
                    //    {
                    //        //await product.FormFile.CopyToAsync(memoryStream);
                    //        NewAddCategory.FileData = memoryStream.ToArray();

                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("File", "The file is too large");

                    //    }
                    //}
                }
                _Category.Insert(NewAddCategory);
                _Category.Save();
            }
            return RedirectToPage("Index");

        }
        //private void SaveDocument(IFormFile document)
        //{
        //    if (IsFileValid(document))
        //    {
        //        Category.FileData = GetFileBytes(document);
        //        Category.FileName = document.FileName;
        //        //CollectionData.FileType = document.ContentType;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Collection Document", "No Document Uploaded");
        //    }
        //}


        private bool IsFileValid(IFormFile document)
        {
            if (document == null || document.Length < 0)
            {
                return false;
            }

            string fileName = document.FileName.ToLower();
            if (fileName.LastIndexOf(".jpeg") <= 0 &&
                fileName.LastIndexOf(".jpg") <= 0 &&
                fileName.LastIndexOf(".png") <= 0 &&
                fileName.LastIndexOf(".bmp") <= 0 &&
                fileName.LastIndexOf(".pdf") <= 0 &&
                fileName.LastIndexOf(".docx") <= 0 &&
                fileName.LastIndexOf(".doc") <= 0 &&
                fileName.LastIndexOf(".xlsx") <= 0 &&
                fileName.LastIndexOf(".txt") <= 0 &&
                fileName.LastIndexOf(".pptx") <= 0 &&
                fileName.LastIndexOf(".ppt") <= 0)
            {
                return false;
            }

            return true;
        }

        private byte[] GetFileBytes(IFormFile file)
        {
            using (var memStream = new MemoryStream())
            {
                file.OpenReadStream().CopyTo(memStream);
                return memStream.ToArray();
            }
        }
        private string ProcessUploadedFile(IFormFile file)
        {
            string uniqueFileName = "";
            string path = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads/Category");
                uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            return uniqueFileName;
        }
    }
}
