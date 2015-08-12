using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    /// <summary>
    /// Base class for viewmodels with Language field.
    /// </summary>
    public class LanguageList
    {
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