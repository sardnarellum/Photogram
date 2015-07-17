using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{
    public class ThumbnailViewModel
    {
        public Media Item { get; set; }

        public IList<Project> Projects { get; set; }
    }
}