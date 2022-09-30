using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GenerikRepositoryPattern.Models
{
    public class MultipleCheckbox
    {

        public  MultipleCheckbox()
        {
             Jobs = new List<SelectListItem>();
        }

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<string> IsChecked { get; set; }
        public List<SelectListItem> Jobs { get; set; }

       
    }
}
