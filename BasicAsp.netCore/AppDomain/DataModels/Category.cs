using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.DataModels
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual IList<Product>? Products { get; set; }

        public string? FileName { get; set; }

        public byte[]? FileData { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }
}
