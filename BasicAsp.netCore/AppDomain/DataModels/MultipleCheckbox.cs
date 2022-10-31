using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppDomain.DataModels
{
    public class MultipleCheckbox
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int SubCheckboxId { get; set; }
       public IList<SubCheckBox>? CheckBoxes { get; set; }

       

       
    }
}
