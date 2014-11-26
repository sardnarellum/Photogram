using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    //[MetadataType(typeof(AddGalleryViewModel))]
    //public partial class Gallery
    //{
    //}

    public class AddGalleryViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Title")]
        [MaxLength(10)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
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