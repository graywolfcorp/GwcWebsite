using System;

namespace GWC.Web.Model
{
    public class BillingCalendar
    {
        public int Id { get; set; }
        public int? BillMonth { get; set; }
        public int? BillYear { get; set; }
        public string Cycle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ImportBillingDate { get; set; }
        public DateTime? BillingEmailCCDate { get; set; }
        public DateTime? StatementDate { get; set; }
        public DateTime? FirstCDRDate { get; set; }
        public DateTime? LoadCDRDate { get; set; }
        public DateTime? LastCDRDate { get; set; }
        public int? BillingDays { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? BlockingDate { get; set; }
        public string MessageId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? StatementMailingDate { get; set; }
    }


}
