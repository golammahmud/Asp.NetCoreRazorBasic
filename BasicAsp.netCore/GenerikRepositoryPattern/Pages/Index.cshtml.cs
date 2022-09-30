using AppDataAccess.GenerikInterface;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace GenerikRepositoryPattern.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        //private IGenerik<MultipleCheckbox> _IMultipleCheckBox;
        //public IndexModel(ILogger<IndexModel> logger, IGenerik<MultipleCheckbox> IMultipleCheckBox)
        //{
        //    this._logger = logger;
        //    this._IMultipleCheckBox = IMultipleCheckBox;
        //}

        [BindProperty]
        public MultipleCheckbox AreChecked { get; set; }

        
        public List<SelectListItem>? JobTypes { get; set; }
        public void OnGet()
        {
            AreChecked = new MultipleCheckbox();
            JobTypes = new List<SelectListItem>()
               {
                    new SelectListItem() { Text="Mechanical", Value="Mechanical" },
                    new SelectListItem() { Text="Electrical", Value="Electrical" },
                    new SelectListItem() { Text="Fluid Power", Value="Fluid Power" },
                    new SelectListItem() { Text="Programming", Value="Programming" }
               };
           
            if (JobTypes!=null)
            {
                AreChecked.Jobs = JobTypes;
            }
           


            if (AreChecked.IsChecked == null)
            {
                AreChecked.IsChecked = new List<string>();
            }
            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            AreChecked = new MultipleCheckbox();
            var data = Request.Form["Fruit"].ToList();
            var isCheked = new List<string>();
            foreach (var item in data)
            {
                isCheked.Add(item);
            }

            AreChecked.IsChecked = isCheked;
            //_IMultipleCheckBox.Insert(AreChecked);
            //_IMultipleCheckBox.Save();
            TempData["message"] = "data saved sucefully!";

            return Page();
        }
    }
}