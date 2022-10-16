using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GenerikRepositoryPattern.Pages.MultiFiles
{
    public class ProductDetailModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        private IGenerik<Product> _IProducts;
        private IGenerik<Category> _Category;
        public const string SessionKeyName = "_Name";
        public ProductDetailModel(AppDbContext appDbContext, IGenerik<Product> iProducts, IGenerik<Category> category, IWebHostEnvironment _webHostEnvironment)
        {
            this._IProducts = iProducts;
            this._Category = category;
            this._appDbContext = appDbContext;
        }
        [BindProperty]
        public ProductViewModel productView { get; set; }
        public void OnGet(int Id)
        {
            productView = new ProductViewModel();
            Product domainProduct = _IProducts.GetById(Id);

           
            //var FileModelItemList = _appDbContext.FileModel.Where(f => f.ProductId == Id).ToList();
            if (domainProduct != null )
            {
                productView.Id = domainProduct.Id;
                productView.Name = domainProduct.Name;
                productView.Price = domainProduct.Price;
                productView.CategoryId = domainProduct?.CategoryId;
                productView.Description = domainProduct.Description;
                productView.File = domainProduct.File;
                productView.FileUrl = domainProduct.FileUrl;
                productView.Files = domainProduct.Files.Select(f => new FileModel()
                {
                    File = f.File,
                    FilePath = f.FilePath,
                }).ToList();
            productView.CreatedAt = domainProduct.CreatedAt;
            }
            
        }
    }
}
