using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace Photogram.WebApp.Models
{
    //[MetadataType(typeof(AddProjectViewModel))]
    //public partial class Project
    //{
    //}

    public class AddProjectViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title", ResourceType=typeof(Localization))]
        [MaxLength(10)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(Localization))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Year", ResourceType = typeof(Localization))]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Language", ResourceType = typeof(Localization))]
        public string Language { get; set; }

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

        public IEnumerable<SelectListItem> Languages
        {
            get
            {
                PhotogramEntities db = new PhotogramEntities();
                var languages = db.Language.OrderByDescending(x => x.Name).ToList();

                return new SelectList(
                    languages.Select(language => new SelectListItem
                    {
                        Value = language.Code,
                        Text = language.Name
                    }
                ), "Value", "Text");
            }
        }
    }
}