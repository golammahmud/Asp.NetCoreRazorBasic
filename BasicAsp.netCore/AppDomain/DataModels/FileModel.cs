using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.DataModels
{
    public class FileModel
    {
        [Key]
        public int FileId { get; set; }
        public string? FilePath { get; set; }

        public Byte[]? File { get; set; }

        public int? ProductId { get; set; }

        public int? CategoryId { get; set; }
    }
}
