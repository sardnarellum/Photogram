using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace Photogram.WebApp.Models
{
    /// <summary>
    /// Fájl tulajdonságaival kapcsolatos információk.
    /// </summary>
    public class MediaInformation : LanguageList
    {
        [Display(Name = "Filename", ResourceType = typeof(Localization))]
        public string FileName { get; set; }

        /// <summary>
        /// Media Id of item which to be updated.
        /// </summary>
        [Required]
        public int MediaId { get; set; }

        /// <summary>
        /// Title of Media item.
        /// </summary>
        [StringLength(20,
            ErrorMessageResourceName = "ErrTitleLength",
            ErrorMessageResourceType = typeof(Localization))]
        [DataType(DataType.Text)]
        [Display(Name = "Title", ResourceType = typeof(Localization))]
        public string Title { get; set; }

        /// <summary>
        /// Description of Media item.
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(Localization))]
        public string Description { get; set; }
    }
}