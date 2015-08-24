using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{

    /// <summary>
    /// General localization selection logic.
    /// </summary>
    public static class LocalizedEntityExtensions
    {
        /// <summary>
        /// Text at first on current language and on english if not exists.
        /// If english version not exists returns the default.
        /// If the collection is empty, returns null.
        /// </summary>
        /// <returns></returns>
        public static T Current<T>(this ICollection<T> collection) where T : Translation
        {
            var language = CurrentOrDefaultLanguage();

            var title = collection
                    .Where(x => x.Language == language)
                    .FirstOrDefault();

            if (null != title)
            {
                return title;
            }

            // If title does not exists with current lang or english or 1033 is deleted from db.
            title = collection.FirstOrDefault();

            return title;
        }

        private static Language CurrentOrDefaultLanguage()
        {
            var db = new PhotogramEntities();

            var language = db.Language
                .Where(x => x.LCID == CultureInfo.CurrentCulture.LCID)
                .FirstOrDefault();

            // If CurrentCulture is set to unknown default language is English
            if (null == language)
            {
                language = db.Language.Where(x => x.LCID == 1033)
                    .FirstOrDefault();
            }

            return language;
        }
    }

    public partial class Project
    {
        /// <summary>
        /// Title text at first on current language and on english if not exists.
        /// If english version not exists returns the default title's text.
        /// If not exists any titles, returns empty string.
        /// </summary>
        /// <returns></returns>
        public string CurrentTitleText()
        {
            var title = Title.Current();

            return null != title ? title.Text : "";
        }

        public string CurrentDescriptionText()
        {
            var description = Description.Current();

            return null != description ? description.Text : "";
        }
    }

    public partial class Media
    {
        /// <summary>
        /// Title at first on current language and on english if not exists.
        /// If english version not exists returns the default.
        /// If no title, returns empty string.
        /// </summary>
        /// <returns></returns>
        public string CurrentTitleText()
        {
            var title = Title.Current();

            return null != title ? title.Text : "";
        }

        public string CurrentDescriptionText()
        {
            var description = Description.Current();

            return null != description ? description.Text : "";
        }
    }

    public partial class Setup
    {
        public string CurrentMainTitleText()
        {
            var mainTitle = MainTitle.Current();

            return null != mainTitle ? mainTitle.Text : "";
        }

        public string CurrentFooterText()
        {
            var footer = Footer.Current();

            return null != footer ? footer.Text : "";
        }
    }
}