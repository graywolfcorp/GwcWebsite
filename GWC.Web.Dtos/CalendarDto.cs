namespace GWC.Web.Dtos
{
    public class CalendarDto
    {
        public int Id { get; set; }
        public string Page { get; set; }
        public string EventDate { get; set; }
        public string Comments { get; set; }
        public bool? Quote { get; set; }
        public bool? Hide { get; set; }
        public int SourceId { get; set; }

        public SourceDto Source { get; set; }
    }
}