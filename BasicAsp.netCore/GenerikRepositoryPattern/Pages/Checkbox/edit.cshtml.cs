using AppDataAccess.Data;
using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GenerikRepositoryPattern.Pages.Checkbox
{
    public class editModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private IGenerik<MultipleCheckbox> _IMultipleCheckBox;
        private IGenerik<SubCheckBox> _SubCheckBox;
        public editModel(ILogger<IndexModel> logger, IGenerik<MultipleCheckbox> IMultipleCheckBox, IGenerik<SubCheckBox> subCheckBox)
        {
            _logger = logger;
            _IMultipleCheckBox = IMultipleCheckBox;
            _SubCheckBox = subCheckBox;
        }

        [BindProperty]
        public MultipleCheckboxViewModel ModelItem { get; set; }

        [BindProperty]
        public IList<SelectListItem> CheckBoxItemList { get; set; } 
        public void OnGet(int Id)
        {

            var ListData = _IMultipleCheckBox.GetById(Id);
            var FileModelItemList = _SubCheckBox.GetAll().Where(c=>c.MultipleCheckboxId==ListData.Id).ToList();
            {

                if (ListData.CheckBoxes != null)
                {
                    CheckBoxItemList = ListData.CheckBoxes.Select(f => new SelectListItem()
                    {
                        Value = f.IsChecked,
                        Text = f.IsChecked,
                    }).ToList();
                }

            }


            Page();
        }

        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ModelItem = new MultipleCheckboxViewModel();
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
