using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{
    public class HomeModel
    {
        public Project[] Projects { get; set; }

        public Language[] Languages { get; set; }
    }
}