
namespace GWC.Web.Dtos.Contentful
{
    public class BlogPostRequestDto
    {
        public string ContentType { get; set; }
        public string Endpoint { get; set; }
        public string Fields { get; set; }
        public string FilterValue { get; set; }
        public string Include { get; set; }
        public string Limit { get; set; }
        public string Order { get; set; }
        //public string Skip { get; set; }
        public int CurrentPage { get; set; }
        
        public BlogPostRequestDto(
            string contentType = "blog",
            string endpoint = "entries",
            string fields = "sys.id,fields",
            string filterValue = "",
            string include = "0",            
            string limit = "0",
            string order = "-fields.date",
            int currentPage = 1
          //  string skip = "0"
            )
        {
            ContentType = contentType;
            Endpoint = endpoint;
            Fields = fields;
            FilterValue = filterValue;
            Include = include;
            Limit = limit;
            Order = order;
            CurrentPage = currentPage;
            //Skip = skip;            
        }

    }
}