using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{
    public class ContentMgmtViewModel
    {
        public IList<Media> Medias { get; set; }

        public IList<Project> Projects { get; set; }
    }
}