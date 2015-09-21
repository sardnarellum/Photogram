using Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    /// <summary>
    /// Base class for viewmodels with Language field.
    /// </summary>
    public class LanguageList
    {
        [Required]
        [Display(Name = "Language", ResourceType = typeof(Localization))]
        public string LCID { get; set; }

        /// <summary>
        /// Creates SelectList with LCID keys and DisplayName values of supperted Cultures.
        /// </summary>
        public IEnumerable<SelectListItem> Languages {  get; set; }
    }
}