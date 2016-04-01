using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photogram.WebApp.Models
{
    public class BlogPostDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Lead { get; set; }

        public string Body { get; set; }

        public DateTime Modified { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public bool Visible { get; set; }
    }

    public class NewBlogPost
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}