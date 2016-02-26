using System;
using System.Collections.Generic;

namespace Photogram.WebApp.Models
{
    public class BlogPostInformation
    {
        public int Id { get; internal set; }

        public string Title { get; internal set; }

        public string Lead { get; internal set; }

        public string Body { get; internal set; }

        public DateTime Modified { get; internal set; }

        public ICollection<Tag> Tags { get; internal set; }

        public bool Visible { get; internal set; }
    }
}