using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GenerikRepositoryPattern.Models
{
    public class MultipleCheckboxViewModel
    {


        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string IsChecked { get; set; }
        public List<SelectListItem> Jobs { get; set; }

       
    }
}
