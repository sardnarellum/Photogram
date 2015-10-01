using PagedList;

namespace Photogram.WebApp.Models
{
    public class ProjectDetailsModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IPagedList<Media> IncludedMedia { get; set; }
    }
}