using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAutoMapperApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public bool? IsOrdered { get; set; }

        public bool? IsNewOreder { get; set; }

        public List<string> jobName { get; set; }

        [NotMapped]
        public List<SelectListItem> jobItem { get; set; }

       
    }
}
