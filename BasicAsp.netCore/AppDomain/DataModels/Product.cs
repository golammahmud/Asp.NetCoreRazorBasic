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
        
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal? Price { get; set; }
        public string? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public string? Description { get; set; }


        public Byte[]? File { get; set; }
        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
