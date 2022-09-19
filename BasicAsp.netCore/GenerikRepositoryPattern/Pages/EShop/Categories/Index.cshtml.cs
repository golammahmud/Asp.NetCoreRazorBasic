using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GenerikRepositoryPattern.Pages.EShop.Categories
{
    public class IndexModel : PageModel
    {
        private AppDbContext _context;
        private IGenerik<Category> _Category;

        private readonly IWebHostEnvironment _hostenvironment;

        public IndexModel(IGenerik<Category> category, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            this._Category = category;
            this._hostenvironment = webHostEnvironment;
            this._context = context;
        }

        [BindProperty]
        public IList<Category> categories { get; set; }
        public void OnGet()
        {
            categories = new List<Category>();
            categories = _context.Category.Include(p=>p.Products).ToList();
            if (categories.Count > 0)
            {
                categories = categories.OrderBy(x => x.Name).ToList();

            }
        }
    }
}
