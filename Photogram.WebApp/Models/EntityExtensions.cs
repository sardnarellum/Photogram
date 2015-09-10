using Resources;
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

            db.Dispose();

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

        /// <summary>
        /// Position for a newly associated element.
        /// </summary>
        /// <returns>The position after the last associated element's position.</returns>
        public int NewPosition()
        {
            var db = new PhotogramEntities();
            var includes = db.ProjectInclude.Where(x => x.Project.Id == Id)
                .ToList();

            db.Dispose();

            return includes.Count > 0 ? includes.Max(x => x.Position) + 1 : 1;
        }

        public void SetPosition(int newPosition)
        {
            if (newPosition == Position)
                return;

            var db = new PhotogramEntities();
            var includes = db.Project.ToList();

            var maxPosition = includes.Max(x => x.Position);

            if (newPosition < 1 || newPosition > maxPosition)
            {
                throw new ArgumentOutOfRangeException(
                    Localization.ErrPosRange);
            }

            var involved = includes.Where(x =>
                    x.Position >= Math.Min(Position, newPosition) &&
                    x.Position <= Math.Max(Position, newPosition) &&
                    x.Position != Position);

            var direction = Position < newPosition ? -1 : 1;

            foreach (var elem in involved)
            {
                elem.Position += direction;
            }

            Position = newPosition;

            db.SaveChanges();

            db.Dispose();
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

    public partial class ProjectInclude
    {
        /// <summary>
        /// Set an elements position in project and shifting the other elements.
        /// </summary>
        /// <param name="newPosition"></param>
        public void SetPosition(int newPosition)
        {
            if (newPosition == Position)
                return;

            var db = new PhotogramEntities();
            var includes = db.ProjectInclude.Include("Project")
                .Where(x => x.Project.Id == Project.Id);

            var maxPosition = includes.Max(x => x.Position);

            if (newPosition < 1 || newPosition > maxPosition)
            {
                throw new ArgumentOutOfRangeException(
                    Localization.ErrPosRange);
            }

            var involved = includes.Where(x =>
                    x.Position >= Math.Min(Position, newPosition) &&
                    x.Position <= Math.Max(Position, newPosition) &&
                    x.Position != Position);

            var direction = Position < newPosition ? -1 : 1;

            foreach (var elem in involved)
            {
                elem.Position += direction;
            }

            Position = newPosition;

            db.SaveChanges();

            db.Dispose();
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