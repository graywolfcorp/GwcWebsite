using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Models;
using GWC.Web.Dtos;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GWC.Web.Dtos.Contentful;
using System.Net.Http.Formatting;

namespace GWC.Web.Testing.IntegrationTesting
{
    public class Product
    {
        public SystemProperties Sys { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
    }

    public class MiscIntegrationFixture : BaseIntegrationTestFixture
    {
        [Test]
        public async Task Contentful_Api()
        {
            var client = new HttpClient();

            MediaTypeFormatterCollection formatters = new MediaTypeFormatterCollection();
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.contentful.delivery.v1+json"));


            var deliveryApiKey = "c66a798c5adb7e11727de7831e48f2cf5039b2c2f2595be6f83b67c03fac3e4a";
            var url = $"https://cdn.contentful.com/spaces/fgj5iptu3uwg/environments/master/entries?links_to_entry=3ZbGNsHyNiWMGs0eC6g6yc&access_token={deliveryApiKey}";

            string test = string.Empty; ;
            var blog = new PostDto();

            var response = await client.GetAsync(url);
            

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    blog = await response.Content.ReadAsAsync<PostDto>(formatters);
                }                
             
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            
            
            Assert.That(test, Is.Not.Null);
        }


        [Test]
        public async Task Contentful()
        {
            var httpClient = new HttpClient();
            var options = new ContentfulOptions()
            {
                DeliveryApiKey = "c66a798c5adb7e11727de7831e48f2cf5039b2c2f2595be6f83b67c03fac3e4a",
                PreviewApiKey = "ae5d997b2a95d49e250939bc350397778bb17ebe56560e8e2442b320668b2550",
                SpaceId = "fgj5iptu3uwg"
            };
            var client = new ContentfulClient(httpClient, options);

            var entry = await client.GetEntry<Product>("3r6V9g1XzGaKAY4syUuyIg");
            Assert.That(entry.Sys.Id, Is.Not.Null);
        }

        [Test]
        public void Logging()
        {
            var log = gwcCommonService.LogManager.GetDefaultDbLoggingService();
            log.Debug("logging test");
        }

        [Test]
        public void LoadSources()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var filePath = @"C:\Users\park\Downloads\Sources.json";

            var reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            var items = JsonConvert.DeserializeObject<List<SourceDto>>(fileContents,settings);

            foreach(var item in items)
            {
                var source = sourceService.AddSource(item);
            }

            Assert.That(items.Count, Is.GreaterThan(0));
        }

        [Test]
        public void LoadCalendar()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var filePath = @"C:\Users\park\Downloads\Calendar.json";

            var reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            var items = JsonConvert.DeserializeObject<List<CalendarDto>>(fileContents, settings);

            foreach (var item in items)
            {
                var calendar = calendarService.AddCalendar(item);
            }

            Assert.That(items.Count, Is.GreaterThan(0));
        }

    }
}
