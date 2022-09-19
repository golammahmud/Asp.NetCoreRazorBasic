using System.ComponentModel.DataAnnotations;

namespace GenerikRepositoryPattern.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }

        [Display(Name = "Category")]
        public string? CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }
        public string? Description { get; set; }



        public Byte[]? File { get; set; }
        public string? FileUrl { get; set; }

        public IFormFile? FormFile { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
