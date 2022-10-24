namespace DemoAutoMapperApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual IList<ProductViewModel>? Products { get; set; }

        public string? FileName { get; set; }

        public byte[]? FileData { get; set; }

        public IFormFile? FormFile { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
