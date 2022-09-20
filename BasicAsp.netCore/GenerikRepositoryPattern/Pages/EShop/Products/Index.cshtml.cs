using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GenerikRepositoryPattern.Pages.EShop.Products
{
    public class IndexModel : PageModel
    {
        private AppDbContext _context;
        private IGenerik<Product> _IProducts;

        private readonly IWebHostEnvironment _hostenvironment;

        public IndexModel(IGenerik<Product> iProducts, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            this._IProducts = iProducts;
            this._hostenvironment = webHostEnvironment;
            this._context = context;
        }

        [BindProperty]
        public IList<Product> products { get; set; }

        public void OnGet()
        {
            products = new List<Product>();
            //products = _IProducts.GetAll().ToList();
            products = _context.Products.Include(c=>c.Category).ToList();/*_context.Product.Include(s => s.Category).ToList();*/  /*AsNoTracking improves performance because the entities returned are not tracked. The entities don't need to be tracked because they're not updated in the current context.*/
            if (products.Count > 0)
            {
                products = products.OrderBy(x => x.Name).ToList();

            }

        }
        public FileResult OnGetDownloadFileFromFolder(string fileName)
        {
            //Build the File Path.
            //string path = Path.Combine(this._hostenvironment.WebRootPath, "FileUploads") + fileName;
            string filePath = Path.Combine(_hostenvironment.WebRootPath, "FileUploads", fileName);
              
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);

        }
        public FileResult OnGetDownloadFileFromDatabase(string fileName)
        {
            var bytes = _context.Products.Where(c => c.FileUrl == fileName).FirstOrDefault().File;

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }
}
