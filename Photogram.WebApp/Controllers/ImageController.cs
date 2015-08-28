using ImageResizer;
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

        public ActionResult RenderResized(int width, int height, string file)
        {
            var fullFilePath = this.GetFullFilePath(file);

            if (this.FileNotAvailable(fullFilePath))
                return this.Instantiate404ErrorResult(file);

            var resizeSettings = InstantiateResizeSettings(width, height);

            using (var resizedImage = ImageBuilder.Current.Build(fullFilePath, resizeSettings))
            {
                return new DynamicImageResult(file, resizedImage.ToByteArray());
            }
        }

        private string GetFullFilePath(string file)
        {
            return string.Concat(
                System.Web.HttpContext.Current.Server.MapPath(Macros.UploadPathImgRel), file);
        }

        private bool FileNotAvailable(string fullFilePath)
        {
            return !System.IO.File.Exists(fullFilePath);
        }

        private ResizeSettings InstantiateResizeSettings(int width, int height)
        {
            return new ResizeSettings(string.Format(
                "maxwidth={0}&maxheight={1}&quality=90", width, height));
        }
        private HttpNotFoundResult Instantiate404ErrorResult(string file)
        {
            return new HttpNotFoundResult(
                string.Format("The file {0} does not exists.", file));
        }
    }
}