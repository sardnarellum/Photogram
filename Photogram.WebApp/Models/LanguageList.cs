using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    /// <summary>
    /// Base class for viewmodels with Language field.
    /// </summary>
    public class LanguageList
    {
        /// <summary>
        /// Creates SelectList with LCID keys and DisplayName values of supperted Cultures.
        /// </summary>
        public IEnumerable<SelectListItem> Languages
        {
            get
            {
                var db = new PhotogramEntities();

                var selectListItems = new List<SelectListItem>();

                foreach (var elem in db.Language)
                {
                    var item = new SelectListItem
                    {
                        Value = elem.LCID.ToString(),
                        Text = LanguageName(elem.LCID)
                    };

                    selectListItems.Add(item);
                }

                var selectList = new SelectList(
                    selectListItems, "Value", "Text");

                return selectList;
            }
        }

        /// <summary>
        /// Returns DisplayName for the given LCID.
        /// </summary>
        /// <param name="lcid"></param>
        /// <returns></returns>
        private string LanguageName(int lcid)
        {
            return CultureInfo.GetCultureInfo(lcid).DisplayName;
        }
    }
}