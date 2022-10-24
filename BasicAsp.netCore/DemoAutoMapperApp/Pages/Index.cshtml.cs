using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using AutoMapper;
using DemoAutoMapperApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoAutoMapperApp.Pages
{
    public class IndexModel : PageModel
    {
        private AppDbContext _context;
        private IGenerik<Product> _IProducts;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostenvironment;

        public IndexModel(IGenerik<Product> iProducts, IWebHostEnvironment webHostEnvironment, AppDbContext context, IMapper mapper)
        {
            this._IProducts = iProducts;
            this._hostenvironment = webHostEnvironment;
            this._context = context;
            this._mapper = mapper;
        }

        [BindProperty]
        public IList<ProductViewModel> products { get; set; }

        public void OnGet()
        {
            products = new List<ProductViewModel>();
           
            var data = _context.Products.Include(c => c.CategoriList).ToList();
            _mapper.Map(data, products);


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