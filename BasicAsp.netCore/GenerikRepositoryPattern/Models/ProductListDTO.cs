namespace GenerikRepositoryPattern.Models
{
    public class ProductListDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal price { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public Byte[] MultiFile { get; set; }
        public Byte[] File { get; set; }
        public string MultiFilePath { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
