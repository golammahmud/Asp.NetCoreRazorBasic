using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GenerikRepositoryPattern.Pages.EShop.Order
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public OrderViewModel AreChecked { get; set; }
        //public void OnGet()
        //{
        //    Orders = new OrderViewModel();
        //}
        public List<SelectListItem> JobTypes { get; set; }
        public void OnGet()
        {
            AreChecked = new OrderViewModel();
            JobTypes = new List<SelectListItem>()
               {
                    new SelectListItem() { Text="Mechanical", Value="Mechanical" },
                    new SelectListItem() { Text="Electrical", Value="Electrical" },
                    new SelectListItem() { Text="Fluid Power", Value="Fluid Power" },
                    new SelectListItem() { Text="Programming", Value="Programming" }
               };
            AreChecked.jobItem = JobTypes;
            Page();
        }

        //public IActionResult OnPost()
        //{
        //    if(ModelState.IsValid != true)
        //    {
        //        return Page();
        //    }
        //    Orders = new OrderViewModel();
        //    return Page();
        //}
    }
   
}
