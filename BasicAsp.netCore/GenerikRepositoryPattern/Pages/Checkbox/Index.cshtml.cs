using AppDataAccess.GenerikInterface;
using AppDomain.DataModels;
using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GenerikRepositoryPattern.Pages.Checkbox
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IGenerik<MultipleCheckbox> _IMultipleCheckBox;
        public IndexModel(ILogger<IndexModel> logger, IGenerik<MultipleCheckbox> IMultipleCheckBox)
        {
            _logger = logger;
            _IMultipleCheckBox = IMultipleCheckBox;
        }

        [BindProperty]
        public IList<MultipleCheckbox> ModelItem { get; set; }


        public void OnGet()
        {


            Page();
        }
    }
}
