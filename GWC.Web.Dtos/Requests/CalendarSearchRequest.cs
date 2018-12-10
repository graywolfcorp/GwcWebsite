using System;

namespace GWC.Web.Dtos.Requests
{
    public class CalendarSearchRequest
    {
        public int[] Sources { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
