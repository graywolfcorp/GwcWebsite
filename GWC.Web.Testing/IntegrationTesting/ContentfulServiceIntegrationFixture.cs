using GWC.Web.Dtos.Contentful;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GWC.Web.Testing.IntegrationTesting
{

    public class ContentfulServiceIntegrationFixture : BaseIntegrationTestFixture
    {
        [Test]
        public async Task Get_Posts_By_Tag()
        {
            var request = new BlogPostRequestDto(order: "-sys.createdAt", filterValue: "3ZbGNsHyNiWMGs0eC6g6yc");
            var blogs = await contentfulService.GetPostsByTag(request);
            Assert.That(blogs.items, Is.LessThan(blogs.total));
        }

        [Test]
        public async Task Get_Posts_By_Year()
        {
            var request = new BlogPostRequestDto(order: "-sys.createdAt", filterValue: "2018");
            var blogs = await contentfulService.GetPostsByYear(request);
            Assert.That(blogs.items, Is.GreaterThan(0));
        }



    }
}
