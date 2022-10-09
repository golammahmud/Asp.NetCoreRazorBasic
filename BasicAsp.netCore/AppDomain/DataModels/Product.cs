using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.DataModels
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? CategoriList { get; set; }
        public string? Description { get; set; }
        public Byte[]? File { get; set; }
        public string? FileUrl { get; set; }

        //Navigation path
       public int? FileId { get; set; }
        public List<FileModel>? Files { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
