using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Photogram.WebApp.Models
{
    public class DynamicImageResult : FileContentResult
    {
        public DynamicImageResult(string fileName, byte[] fileData) : 
            base(fileData, string.Format("image/{0}",
            fileName.FileExtensionForContentType())) { }

        protected override void WriteFile(HttpResponseBase response)
        {
            response.SetDefaultImageHeaders();
            base.WriteFile(response);
        }
    }
}