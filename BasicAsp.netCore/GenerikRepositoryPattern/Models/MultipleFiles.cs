using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GenerikRepositoryPattern.Models
{
    public class MultipleFiles
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; } // convert to list
        public string SuccessMessage { get; set; }
    }
}
