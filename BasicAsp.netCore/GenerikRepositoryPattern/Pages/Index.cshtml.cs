using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace GenerikRepositoryPattern.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IGenerik<MultipleCheckbox> _IMultipleCheckBox;
        public IndexModel(ILogger<IndexModel> logger, IGenerik<MultipleCheckbox> IMultipleCheckBox)
        {
            this._logger = logger;
            this._IMultipleCheckBox = IMultipleCheckBox;
        }

        [BindProperty]
        public MultipleCheckboxViewModel CheckedItems { get; set; }
        public List<SelectListItem> JobTypes { get; set; }= new List<SelectListItem>();
        public void OnGet()
        {
            CheckedItems = new MultipleCheckboxViewModel();
            JobTypes = new List<SelectListItem>()
               {
                    new SelectListItem() { Text="Mechanical", Value="Mechanical" },
                    new SelectListItem() { Text="Electrical", Value="Electrical" },
                    new SelectListItem() { Text="Fluid Power", Value="Fluid Power" },
                    new SelectListItem() { Text="Programming", Value="Programming" }
               };
           
            if (JobTypes!=null)
            {
                CheckedItems.Jobs = JobTypes;
            }

            RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {


            CheckedItems = new MultipleCheckboxViewModel();
            var data = Request.Form["Fruit"].ToList();
            
            if (data.Count > 0)
            {
                if (data != null)
                {
                    CheckedItems.IsChecked = String.Join(",",data);
                }
            }


         
            TempData["message"] = "data saved sucefully!";

            return RedirectToPage("Index");
        }
    }
}