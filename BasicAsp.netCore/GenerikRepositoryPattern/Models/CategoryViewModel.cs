namespace GenerikRepositoryPattern.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual IList<ProductViewModel>? Products { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
