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
        public MultipleCheckbox ModelItem { get; set; } = default!;

        [BindProperty]
        public IList<SelectListItem> JobList { get; set; } = default!;
        public void OnGet()
        {
            ModelItem = new MultipleCheckbox();
            JobList = new List<SelectListItem>();
            JobList = new List<SelectListItem>()
               {
                    new SelectListItem() { Text="Mechanical", Value="Mechanical" },
                    new SelectListItem() { Text="Electrical", Value="Electrical" },
                    new SelectListItem() { Text="Fluid Power", Value="Fluid Power" },
                    new SelectListItem() { Text="Programming", Value="Programming" }
               };

            //if (JobList.Count > 0 && JobList!=null)
            //{
            //    ModelItem.jobTypeList = JobList;
            //}
          
            if(ModelItem.IsChecked ==null){
                ModelItem = new MultipleCheckbox();
            }
            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ModelItem = new MultipleCheckbox();
                var data = Request.Form["Fruit"].ToList();

                if (data != null)
                {
                    ModelItem.IsChecked = String.Join("@@", data);
                }
                //_IMultipleCheckBox.Insert(ModelItem);
                //_IMultipleCheckBox.Save();
                TempData["message"] = "data saved sucefully!";
            }
            return Page();
        }
    }
}