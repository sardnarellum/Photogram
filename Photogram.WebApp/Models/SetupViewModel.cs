using System.ComponentModel.DataAnnotations;
using Resources;

namespace Photogram.WebApp.Models
{
    public class SetupViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email", ResourceType = typeof(Localization))]
        public string Email { get; set; }
    }
}