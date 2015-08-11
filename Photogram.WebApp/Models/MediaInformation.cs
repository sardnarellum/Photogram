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
    public class MediaInformation
    {
        [StringLength(80,
            ErrorMessageResourceName = "ErrTitleLength",
            ErrorMessageResourceType = typeof(Localization))]
        [DataType(DataType.Text)]
        [Display(Name = "Title", ResourceType = typeof(Localization))]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(Localization))]
        public string Description { get; set; }
    }
}