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
    
    public partial class ProjectDescription
    {
        private int ProjectId { get; set; }
        private int LCID { get; set; }
        public string Text { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Project Project { get; set; }
    }
}
