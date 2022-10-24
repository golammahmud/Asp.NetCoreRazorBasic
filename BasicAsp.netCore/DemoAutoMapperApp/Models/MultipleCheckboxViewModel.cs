using AppDomain.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAutoMapperApp.Models
{
    public class MultipleCheckboxViewModel
    {


        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

       
    }
}
