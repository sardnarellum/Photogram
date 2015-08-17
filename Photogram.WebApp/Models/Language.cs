//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Photogram.WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Language
    {
        public Language()
        {
            this.ProjectTitle = new HashSet<ProjectTitle>();
            this.ProjectDescription = new HashSet<ProjectDescription>();
            this.MediaTitle = new HashSet<MediaTitle>();
            this.MediaDescription = new HashSet<MediaDescription>();
            this.SetupMainTitle = new HashSet<SetupMainTitle>();
            this.SetupFooter = new HashSet<SetupFooter>();
        }
    
        public int LCID { get; set; }
    
        public virtual ICollection<ProjectTitle> ProjectTitle { get; set; }
        public virtual ICollection<ProjectDescription> ProjectDescription { get; set; }
        public virtual ICollection<MediaTitle> MediaTitle { get; set; }
        public virtual ICollection<MediaDescription> MediaDescription { get; set; }
        public virtual ICollection<SetupMainTitle> SetupMainTitle { get; set; }
        public virtual ICollection<SetupFooter> SetupFooter { get; set; }
    }
}
