using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace GenerikRepositoryPattern.Pages.EShop.Products
{
    public class AddProductModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private IGenerik<Product> _IProducts;
        private IGenerik<Category> _Category;

        public AddProductModel(AppDbContext appDbContext, IGenerik<Product> iProducts, IGenerik<Category> category, IWebHostEnvironment _webHostEnvironment)
        {
            this._IProducts = iProducts;
            this._Category = category;
            this.webHostEnvironment = _webHostEnvironment;
            this._appDbContext = appDbContext;
        }
        public IList<FileModel> FileNames { get; set; }
        [BindProperty]
        public ProductViewModel product { get; set; }

        [BindProperty]
        public Product UpdateProduct { get; set; }

        public List<SelectListItem> Options { get; set; }
        public void OnGet(int? id)
        {

            // if id parameter has value, retrieve the existing
            product = new ProductViewModel();
            if (id.HasValue)
            {

                Product domainProduct = _IProducts.GetById(id.Value);

                var FileModelItemList = _appDbContext.FileModel.Where(f => f.ProductId == id.Value).ToList();
                if (domainProduct != null || FileModelItemList != null)
                {
                    product.Id = id.Value;
                    product.Name = domainProduct.Name;
                    product.Price = domainProduct.Price;
                    product.CategoryId = domainProduct.CategoryId;
                    product.Description = domainProduct.Description;
                    product.File = domainProduct.File;
                    product.FileUrl = domainProduct.FileUrl;
                    product.Files = (IList<FileModel>?)FileModelItemList;
                    product.CreatedAt = domainProduct.CreatedAt;

                }
            }
            Options = _Category.GetAll().Select(a =>
                                   new SelectListItem
                                   {
                                       Value = a.Id.ToString(),
                                       Text = a.Name
                                   }).ToList();
            if (Options == null)
            {
                Options = new List<SelectListItem>();
            }

            if (product == null)
            {
                NotFound();
            }

            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                if (product.Id > 0)
                {
                    if (product.CategoryId == null)
                    {
                        ModelState.AddModelError(string.Empty, "category is required");
                        return Page();
                    }

                    UpdateProduct = _IProducts.GetById(product.Id);
                    var FileModelItemList = _appDbContext.FileModel.Where(f => f.ProductId == UpdateProduct.Id).ToList();
                    {
                        UpdateProduct.Id = product.Id;
                        UpdateProduct.Name = product.Name;
                        UpdateProduct.Price = product.Price;
                        UpdateProduct.CategoryId = product.CategoryId;
                        UpdateProduct.Description = product.Description;
                        UpdateProduct.CreatedAt = (DateTime?)product.CreatedAt;
                    }
                    //uploads file to folder
                    if (product.FormFiles != null)
                    {

                        if (IsFileValid(product.FormFiles))
                        {
                            string updateFolder = "MultipleFileUploads/Products";
                            if (UpdateProduct.Files != null)
                            {
                                foreach (var items in UpdateProduct.Files)
                                {
                                    if (items.FilePath != null)
                                    {
                                        string uploadedFile = Path.Combine(webHostEnvironment.WebRootPath, "MultipleFileUploads/Products", items.FilePath);
                                        System.IO.File.Delete(uploadedFile);
                                    }
                                }
                            }
                            UpdateProduct.Files = new List<FileModel>();
                            foreach (var item in product.FormFiles)
                            {
                                var files = new FileModel()
                                {
                                    FilePath = await UploadFile(updateFolder, item)
                                };
                                UpdateProduct.Files.Add(files);
                            }
                            
                            foreach (var file in product.FormFiles)
                            {
                                var fileBytesStream = GetFileBytes(file);
                                if (fileBytesStream.Length < 2097152)
                                {
                                    var files = new FileModel()
                                    {
                                        File = fileBytesStream
                                    };
                                    UpdateProduct.Files.Add(files);
                                }
                            }
                        }
                    }

                    _IProducts.Update(UpdateProduct);
                    _IProducts.Save();
                    return RedirectToPage("ProductList");

                }
                else
                {

                    Product newProduct = new Product();
                    {
                        newProduct.Id = product.Id;
                        newProduct.Name = product.Name;
                        newProduct.Price = product.Price;
                        newProduct.CategoryId = product.CategoryId;
                        newProduct.Description = product.Description;
                        newProduct.CreatedAt = (DateTime?)product.CreatedAt;
                    }

                    if (product.FormFiles != null && product.FormFiles.Count > 0)
                    {
                        if (IsFileValid(product.FormFiles))
                        {
                            //uploads file to folder
                            string folder = "MultipleFileUploads/Products";
                            newProduct.Files = new List<FileModel>();
                            foreach (var item in product.FormFiles)
                            {
                                var files = new FileModel()
                                {
                                    FilePath = await UploadFile(folder, item)
                                };
                                newProduct.Files.Add(files);
                            }
                            foreach (var file in product.FormFiles)
                            {
                                var ByteFile = GetFileBytes(file);
                                if (ByteFile.Length < 2097152)
                                {
                                    var byteArray = new FileModel()
                                    {
                                        File = ByteFile,
                                    };
                                    newProduct.Files.Add(byteArray);
                                }

                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Collection Document", "No Document Uploaded");
                        }
                    }
                    _IProducts.Insert(newProduct);
                    _IProducts.Save();
                    TempData["Message"] = "Client  has Been Added Successfully";
                }
                return RedirectToPage("ProductList");
            }

            return Page();
        }
        //private void SaveDocument(IFormFile document)
        //{
        //    if (IsFileValid(document))
        //    {
        //        product.File = GetFileBytes(document);
        //        product.FileUrl = document.FileName;
        //        //CollectionData.FileType = document.ContentType;
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("Collection Document", "No Document Uploaded");
        //    }
        //}


        private bool IsFileValid(List<IFormFile> document)
        {
            foreach (IFormFile file in document)
            {
                if (file == null || file.Length < 0)
                {
                    return false;
                }

                string fileName = file.FileName.ToLower();
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
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads");


                uniqueFileName = file.FileName + "_" + Guid.NewGuid().ToString();
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return uniqueFileName;
        }
        private void ProcessUploadedMultipleFile(List<IFormFile> file, int id)
        {
            string uniqueFileName = "";
            string path = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (file != null)
            {
                string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "MultipleFileUploads/Products");
                FileNames = new List<FileModel>();
                foreach (IFormFile item in file)
                {
                    uniqueFileName = item.FileName + "_" + Guid.NewGuid().ToString();
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(stream);
                        FileNames.Add(new FileModel
                        {
                            ProductId = id,
                            FilePath = uniqueFileName
                        });
                    }
                }

            }

            //return uniqueFileName;
        }
        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            string UniqueName = "";
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, folderPath);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            UniqueName += Guid.NewGuid().ToString() + "_" + file.FileName;
            string UplodedFile = Path.Combine(filePath, UniqueName);
            await file.CopyToAsync(new FileStream(UplodedFile, FileMode.Create));
            return "/" + UniqueName;
        }
    }
}

