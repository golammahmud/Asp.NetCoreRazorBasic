using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.DataModels
{
    public class SubCheckBox
    {
        [Key]
        public int Id { get; set; }

        public int MultiCheckboxId { get; set; }

        public string? IsChecked { get; set; }
    }
}
