namespace GWC.Web.Dtos.Contentful
{
    public class PostDto
    {
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
        public Item[] items { get; set; }


        public class Item
        {
            public Fields fields { get; set; }
        }

        public class Fields
        {
            public string title { get; set; }
            public string date { get; set; }
            public string content { get; set; }
            public Tag[] tags { get; set; }
            public string url { get; set; }
            public string gistId { get; set; }
        }

        public class Tag
        {
            public Sys sys { get; set; }
        }

        public class Sys
        {
            public string type { get; set; }
            public string linkType { get; set; }
            public string id { get; set; }
        }

    }
}



