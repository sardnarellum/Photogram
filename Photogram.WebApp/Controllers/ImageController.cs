using Photogram.WebApp.Models;
using Resources;
using System.Web.Mvc;

namespace Photogram.WebApp.Controllers
{
    public class ImageController :Controller
    {
        public ActionResult Render(string file)
        {
            var fullFilePath = this.GetFullFilePath(file);

            if (this.FileNotAvailable(fullFilePath))
                return this.Instantiate404ErrorResult(file);

            return new ImageFileResult(fullFilePath);
        }

        private string GetFullFilePath(string file)
        {
            return string.Concat(
                System.Web.HttpContext.Current.Server.MapPath(Common.UploadPathImgRel), file);
        }

        private bool FileNotAvailable(string fullFilePath)
        {
            return !System.IO.File.Exists(fullFilePath);
        }

        private HttpNotFoundResult Instantiate404ErrorResult(string file)
        {
            return new HttpNotFoundResult(
                string.Format("The file {0} does not exists.", file));
        }
    }
}