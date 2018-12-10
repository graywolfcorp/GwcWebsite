namespace GWC.Web.Dtos
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Editor { get; set; }
        public string Title { get; set; }
        public string Volume { get; set; }
        public string Publisher { get; set; }
        public string Date { get; set; }
        public int? CD { get; set; }
        public int? PrintPages { get; set; }
        public string Advertisement { get; set; }
        public int? Newspaper { get; set; }
        public int SourceId { get; set; }
        public int ExternalId { get; set; }
    }
}
