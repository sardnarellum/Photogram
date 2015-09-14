using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace Photogram.WebApp.Models
{
    public class ProjectViewModel
    {
        public ProjectProperties Properties { get; set; }

        public ProjectInclude Includes { get; set; }
    }
}