using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWC.Web.Model
{
    public class Source
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Author { get; set; }
        [StringLength(100)]
        public string Editor { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public string Volume { get; set; }
        [StringLength(100)]
        public string Publisher { get; set; }
        public string Date { get; set; }
        public int? CD { get; set; }
        public int? PrintPages { get; set; }
        [StringLength(1000)]
        public string Advertisement { get; set; }
        public int? Newspaper { get; set; }
        public int ExternalId { get; set; }
    }
}
