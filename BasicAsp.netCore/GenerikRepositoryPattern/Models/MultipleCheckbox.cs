using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenerikRepositoryPattern.Models
{
    public class MultipleCheckbox
    {

      

        [Key]
        public int Id { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage ="Name field is required !")]
        public string? Name { get; set; }

        public  string? Category { get; set; }
        public string? IsChecked { get; set; }
      


    }
}
