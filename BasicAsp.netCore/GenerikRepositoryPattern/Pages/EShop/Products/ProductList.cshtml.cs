using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GenerikRepositoryPattern.Pages.EShop.Products
{
    public class ProductListModel : PageModel
    {
        private AppDbContext _context;
        private IGenerik<Product> _IProducts;

        private readonly IWebHostEnvironment _hostenvironment;

        public ProductListModel(IGenerik<Product> iProducts, IWebHostEnvironment webHostEnvironment, AppDbContext context)
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
            products = _context.Products.Include(c => c.CategoriList).ToList();/*_context.Product.Include(s => s.Category).ToList();*/  /*AsNoTracking improves performance because the entities returned are not tracked. The entities don't need to be tracked because they're not updated in the current context.*/
            if (products.Count > 0)
            {
                products = products.OrderBy(x => x.Name).ToList();

            }

        }
    }
}
