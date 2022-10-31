using AppDataAccess.GenerikInterface;
using AppDataAccess.Migrations;
using AppDomain.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GenerikRepositoryPattern.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;
        private IGenerik<MultipleCheckbox> _IMultipleCheckBox;
        public AddModel(ILogger<AddModel> logger, IGenerik<MultipleCheckbox> IMultipleCheckBox)
        {
            this._logger = logger;
            this._IMultipleCheckBox = IMultipleCheckBox;
        }
        [BindProperty]
        public MultipleCheckbox ModelItem { get; set; }
        [BindProperty]
        public IList<SelectListItem> CheckboxItems { get; set; }


        public void OnGet()
        {
            ModelItem = new MultipleCheckbox();

           CheckboxItems = new List<SelectListItem>()
               {
                    new SelectListItem() { Text="Mechanical", Value="Mechanical" },
                    new SelectListItem() { Text="Electrical", Value="Electrical" },
                    new SelectListItem() { Text="Fluid Power", Value="Fluid Power" },
                    new SelectListItem() { Text="Programming", Value="Programming" }
               };
            Page();
        }

        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ModelItem = new MultipleCheckbox();
                var data = Request.Form["Fruit"].ToList();
                var name = Request.Form["name"];
                ModelItem.CheckBoxes = new List<SubCheckBox>();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var files = new SubCheckBox()
                        {
                            IsChecked = item,
                        };
                        ModelItem.CheckBoxes.Add(files);
                    }
                }
                ModelItem.Name = name;
                _IMultipleCheckBox.Insert(ModelItem);
                _IMultipleCheckBox.Save();
                TempData["message"] = "data saved sucefully!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}