using System;
using System.ComponentModel.DataAnnotations;

namespace GWC.Web.Model
{
    /// <summary>  
    /// Calendar Modal  
    /// </summary>  
    public class Calendar
    {
        public int Id { get; set; }
        /// <summary>  
        /// Event Date  
        /// </summary>  
        public DateTime EventDate { get; set; }
        public int SourceId { get; set; }
        public string Page { get; set; }        
        [StringLength(500)]
        public string Comments { get; set; }
        public bool? Quote { get; set; }
        public bool? Hide { get; set; }
        public Source Source { get; set; }
    }
}
