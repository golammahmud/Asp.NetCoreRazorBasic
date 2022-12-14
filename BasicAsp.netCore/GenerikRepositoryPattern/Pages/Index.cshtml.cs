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
        public MultipleCheckboxViewModel ModelItem { get; set; } = default!;

        [BindProperty]
        public IList<SelectListItem> JobList { get; set; } = default!;
        public void OnGet()
        {
            ModelItem = new MultipleCheckboxViewModel();
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
                ModelItem = new MultipleCheckboxViewModel();
            }
            Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ModelItem = new MultipleCheckboxViewModel();
                var data = Request.Form["Fruit"].ToList();

                if (data != null)
                {
                    ModelItem.IsChecked = String.Join("@@", data);
                }
                MultipleCheckbox InsertData = new MultipleCheckbox();
                {
                    InsertData.Id = ModelItem.Id;
                    InsertData.Name=ModelItem.Name;
                    InsertData.IsChecked=ModelItem.IsChecked;
                    InsertData.Category=ModelItem.Category;
                    
                   
                }
                _IMultipleCheckBox.Insert(InsertData);
                _IMultipleCheckBox.Save();
                TempData["message"] = "data saved sucefully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}