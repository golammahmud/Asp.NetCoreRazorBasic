using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.DataModels
{
    public class MultipleCheckbox
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Category { get; set; }
        public string? IsChecked { get; set; }

    }
}
