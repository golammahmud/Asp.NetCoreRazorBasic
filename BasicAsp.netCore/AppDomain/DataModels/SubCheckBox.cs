using System.ComponentModel.DataAnnotations;

namespace AppDomain.DataModels
{
    public class SubCheckBox
    {
        [Key]
        public int Id { get; set; }

        public int MultipleCheckboxId { get; set; }

        public string? IsChecked { get; set; }
    }
}
