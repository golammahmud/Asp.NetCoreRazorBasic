using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GenerikRepositoryPattern.Pages.EShop.Categories
{
    public class AddModel : PageModel
    {

        private IGenerik<Category> _Category;

        public AddModel(IGenerik<Category> category)
        {
            this._Category = category;
        }

        [BindProperty]
        public CategoryViewModel Category { get; set; }
        public void OnGet(int? id)
        {
           
            if (id.HasValue)
            {
                var DomainCategory = _Category.GetById(id.Value);
                {
                    Category.Name = DomainCategory.Name;
                    Category.Products = (IList<ProductViewModel>?)DomainCategory.Products;
                    Category.CreatedAt = DomainCategory.CreatedAt;

                }
                
            }
             Category = new CategoryViewModel();

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid != true)
            {
                return Page();
            }
            if (Category.Id > 0)
            {
                Category UpdateCategory = new Category();
                {
                    UpdateCategory.Name = Category.Name;
                    UpdateCategory.CreatedAt = Category.CreatedAt;
                };

                _Category.Update(UpdateCategory);
                _Category.Save();
            }
            else
            {
                Category NewAddCategory = new Category();
                {
                    NewAddCategory.Name = Category.Name;
                    NewAddCategory.CreatedAt = Category.CreatedAt;
                };
                _Category.Insert(NewAddCategory);
                _Category.Save();
            }
            return RedirectToPage("Index");

        }
    }
}
