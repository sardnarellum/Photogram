using Resources;
using System;
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

        /// <exception cref="CultureNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetCulture(int lcid)
        {
            var db = new PhotogramEntities();
            try
            {
                var cultureInfo = new CultureInfo(lcid);
                var language = db.Language.Where(x => x.LCID == cultureInfo.LCID).FirstOrDefault();

                if (null != language)
                {
                    SetCulture(language);
                }
            }
            finally
            {
                db.Dispose();
                // It is a hotfix now. Needs better solution.
            }
        }

        /// <summary>
        /// Sets the current thread's culture if the given language is available.
        /// </summary>
        /// <param name="lang"></param>
        /// <exception cref="CultureNotFoundException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetCulture(string lang)
        {
            var db = new PhotogramEntities();
            try
            {
                var cultureInfo = new CultureInfo(lang);
                var language = db.Language.Where(x => x.LCID == cultureInfo.LCID).FirstOrDefault();

                if (null != language)
                {
                    SetCulture(language);
                }
            }
            finally
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Sets the current thread's culture.
        /// </summary>
        /// <exception cref="CultureNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void SetCulture(Language lang)
        {
            var cultureInfo = new CultureInfo(lang.LCID);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture =
            CultureInfo.CreateSpecificCulture(cultureInfo.Name);
        }
    }
}