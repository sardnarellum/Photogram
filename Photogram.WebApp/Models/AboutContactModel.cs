using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{
    public class LeadModel
    {
        public string Lead { get; set; }
    }

    public class AboutModel : LeadModel
    {

        public string Body { get; set; }
    }

    public class ContactModel : LeadModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string FacebookURL { get; set; }

        public string GitHubURL { get; set; }

        public string InstagramURL { get; set; }

        public string LinkedInURL { get; set; }
    }
}