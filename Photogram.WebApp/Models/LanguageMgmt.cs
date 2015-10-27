using Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
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
        public int LCID { get; set; }

        /// <summary>
        /// Creates SelectList with LCID keys and DisplayName values of supperted Cultures.
        /// </summary>
        public IEnumerable<SelectListItem> Languages {  get; set; }
    }

    public static class CultureManagement
    {
        public static void SetCulture(int lcid)
        {
            var cultureInfo = new CultureInfo(lcid);
            var db = new PhotogramEntities();
            var language = db.Language.Where(x => x.LCID == cultureInfo.LCID).FirstOrDefault();

            db.Dispose();

            if (null != language)
            {
                SetCulture(language);
            }
        }

        public static void SetCulture(string lang)
        {
            var cultureInfo = new CultureInfo(lang);
            var db = new PhotogramEntities();
            var language = db.Language.Where(x => x.LCID == cultureInfo.LCID).FirstOrDefault();

            db.Dispose();

            if (null != language)
            {
                SetCulture(language);
            }
        }

        public static void SetCulture(Language lang)
        {
            var cultureInfo = new CultureInfo(lang.LCID);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}