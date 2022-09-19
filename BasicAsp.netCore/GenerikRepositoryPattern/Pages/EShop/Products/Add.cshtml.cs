using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using static System.Net.WebRequestMethods;

namespace GenerikRepositoryPattern.Pages.EShop.Products
{
    public class AddModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private IGenerik<Product> _IProducts;
        private IGenerik<Category> _Category;

        public AddModel(IGenerik<Product> iProducts, IGenerik<Category> category, IWebHostEnvironment _webHostEnvironment)
        {
            this._IProducts = iProducts;
            this._Category = category;
            this.webHostEnvironment = _webHostEnvironment;
        }

        [BindProperty]
        public ProductViewModel product { get; set; }

        public List<SelectListItem> Options { get; set; }
        public void OnGet(int? id)
        {

            // if id parameter has value, retrieve the existing
            product = new ProductViewModel();
            if (id.HasValue)
            {
                Product domainProduct = _IProducts.GetById(id.Value);
                if (domainProduct != null)
                {
                    product.Id = id.Value;
                    product.Name = domainProduct.Name;
                    product.Price = domainProduct.Price;
                    product.CategoryId = domainProduct.CategoryId;
                    product.Description = domainProduct.Description;
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
                    if (product.Category == null)
                    {
                        ModelState.AddModelError(string.Empty, "category is required");
                        return Page();
                    }
                    Product domainProduct = new Product();
                    {
                        domainProduct.Id = product.Id;
                        domainProduct.Name = product.Name;
                        domainProduct.Price = product.Price;
                        domainProduct.CategoryId = product.CategoryId;
                        domainProduct.Description = product.Description;
                        domainProduct.CreatedAt = (DateTime)product.CreatedAt;
                    }
                    _IProducts.Update(domainProduct);
                    _IProducts.Save();

                }
                else
                {
                    Product domainProduct = new Product();
                    {
                        domainProduct.Id = product.Id;
                        domainProduct.Name = product.Name;
                        domainProduct.Price = product.Price;
                        domainProduct.CategoryId = product.CategoryId;
                        domainProduct.Description = product.Description;
                        domainProduct.CreatedAt = (DateTime)product.CreatedAt;
                    }
                    //uploads file to folder
                    if (product.FormFile.Length > 0)
                    {
                        string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "FileUploads");

                        string uniqueFileName = Guid.NewGuid().ToString() + "-" + product.FormFile.FileName;

                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await product.FormFile.CopyToAsync(stream);
                            domainProduct.FileUrl = uniqueFileName;
                        }

                        //uploads file to database
                                //SaveDocument(product.FormFile);
                        //or
                        using (var memoryStream = new MemoryStream())
                        {
                            await product.FormFile.CopyToAsync(memoryStream);
                            if (memoryStream.Length < 2097152)
                            {
                                
                                await product.FormFile.CopyToAsync(memoryStream);
                                domainProduct.File = memoryStream.ToArray();

                            }
                            else
                            {
                                ModelState.AddModelError("File", "The file is too large");

                            }
                        }
                    }
                    _IProducts.Insert(domainProduct);
                    _IProducts.Save();
                    TempData["Message"] = "Client  has Been Added Successfully";
                }
                return RedirectToPage("Index");
            }

            return Page();
        }
        private void SaveDocument(IFormFile document)
        {
            if (IsFileValid(document))
            {
                product.File = GetFileBytes(document);
                product.FileUrl = document.FileName;
                //CollectionData.FileType = document.ContentType;
            }
            else
            {
                ModelState.AddModelError("Collection Document", "No Document Uploaded");
            }
        }


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
    }
}
