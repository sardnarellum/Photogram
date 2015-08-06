namespace Photogram.WebApp.Models
{
    /// <summary>
    /// Stores basic information about files.
    /// </summary>
    public class BasicFileData
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectTitle { get; set; }
    }
}