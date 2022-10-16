using GenerikRepositoryPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GenerikRepositoryPattern.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public List<MultipleCheckboxViewModel> CheckedItems { get; set; }
        public void OnGet()
        {
            CheckedItems = CheckedItems.ToList();
        }
    }
}