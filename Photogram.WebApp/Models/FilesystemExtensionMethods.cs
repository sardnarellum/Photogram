using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Photogram.WebApp.Models
{
    public static class FilesystemExtensionMethods
    {
        public static string FileExtensionForContentType(this string fileName)
        {
            var pieces = fileName.Split('.');
            var extension = pieces.Length > 1 ? pieces[pieces.Length - 1]
                : string.Empty;

            return (extension.ToLower() == "jpg") ? "jpeg" : extension;
        }

        public static byte[] ToByteArray(this Bitmap image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Bmp);
                return memoryStream.ToArray();
            }
        }
    }
}