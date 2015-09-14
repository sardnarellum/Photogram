using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    public class ProjectProperties : LanguageList
    {
        [Required]
        [Editable(false)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title", ResourceType = typeof(Localization))]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(Localization))]
        [Editable(false)]
        [MaxLength(2000)]
        public string Slug { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(Localization))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Year", ResourceType = typeof(Localization))]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Language", ResourceType = typeof(Localization))]
        public int LCID { get; set; }

        [Required]
        [Display(Name = "Visibility", ResourceType = typeof(Localization))]
        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Years
        {
            get
            {
                return new SelectList(
                    Enumerable.Range(1989, DateTime.Now.Year + 1 - 1989)
                    .OrderByDescending(year => year)
                    .Select(year => new SelectListItem
                    {
                        Value = year.ToString(),
                        Text = year.ToString()
                    }
                ), "Value", "Text");
            }
        }
    }
}