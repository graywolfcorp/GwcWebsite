namespace GWC.Web.Dtos.Contentful
{
    public class TagDto
    {
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
        public Item[] items { get; set; }
        
        public class Item
        {
            public Fields fields { get; set; }
            public Sys sys { get; set; }
        }

        public class Fields
        {
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class Sys
        {
            public string id { get; set; }
        }

    }
}



