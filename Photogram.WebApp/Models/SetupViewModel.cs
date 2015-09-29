using System.ComponentModel.DataAnnotations;
using Resources;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    public class SetupViewModel : LanguageList
    {
        [Required]
        [Display(Name = "Published", ResourceType = typeof(Localization))]
        public bool Published { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Localization))]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNo", ResourceType = typeof(Localization))]
        public string Phone { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Facebook URL")]
        public string FacebookURL { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Instagram URL")]
        public string InstagramURL { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "GitHub URL")]
        public string GitHubURL { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "LinkedIn URL")]
        public string LinkedInURL { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Display(Name = "MainTitle", ResourceType = typeof(Localization))]
        public string MainTitle { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(200)]
        [Display(Name = "Footer", ResourceType = typeof(Localization))]
        public string Footer { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(300)]
        [Display(Name = "AboutLead", ResourceType = typeof(Localization))]
        public string AboutLead { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        [Display(Name = "AboutBody", ResourceType = typeof(Localization))]
        public string AboutBody { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(300)]
        [Display(Name = "ContactLead", ResourceType = typeof(Localization))]
        public string ContactLead { get; set; }
    }
}