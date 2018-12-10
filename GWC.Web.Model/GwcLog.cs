using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWC.Web.Model
{
    public class GwcLog
    {
        [Key]
        public int Id { get; set; }

        public DateTime LogDate { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(255)]
        public string Logger { get; set; }

        [StringLength(255)]
        public string Method { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(5000)]
        public string Exception { get; set; }

        [StringLength(20)]
        public string Line { get; set; }

        [StringLength(4000)]
        public string Data { get; set; }
    }
}
