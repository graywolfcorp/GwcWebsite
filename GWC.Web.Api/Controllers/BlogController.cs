using Gwc.Common.Utilities.Extensions;
using GWC.Web.Dtos.Contentful;
using GWC.Web.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace GWC.Web.Api.Controllers
{
    /// <summary>  
    /// Blogs  
    /// </summary>  
    [ApiController]
    [Route("api/blogs/posts")]
    public class BlogController : Controller
    {
        private IContentfulService _contentfulService;
        private const string Posts_Per_Page = "5";
        /// <summary>  
        /// Constructor for Blog Controller  
        /// </summary>  
        public BlogController(IContentfulService contentfulService)
        {
            _contentfulService = contentfulService;
        }
       
        [HttpGet("tags/{tag}/{page}")]
        public async Task<IActionResult> GetByTag(string tag, string page)
        {
            var request = new BlogPostRequestDto(
                currentPage: page.IsNumeric() ? page.ToInt() : 1,
                filterValue: tag,
                limit: Posts_Per_Page);
            var userDto = await _contentfulService.GetPostsByTag(request);
            return Ok(userDto);
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var userDto = await _contentfulService.GetTags();
            return Ok(userDto);
        }

        [HttpGet("urls/{url}")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            var userDto = await _contentfulService.GetPostByUrl(url);
            return Ok(userDto);
        }

        [HttpGet("years/{year}/{page}")]
        public async Task<IActionResult> GetByYear(string year, string page)
        {
            var request = new BlogPostRequestDto(
                currentPage: page.IsNumeric() ? page.ToInt() : 1,
                filterValue: year,
                limit: Posts_Per_Page);
            var blogDto = await _contentfulService.GetPostsByYear(request);
            return Ok(blogDto);
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetPosts(string page)
        {
            var request = new BlogPostRequestDto(
                currentPage: page.IsNumeric() ? page.ToInt() : 1,
                limit: Posts_Per_Page);
            var userDto = await _contentfulService.GetRecentPosts(request);
            return Ok(userDto);
        }

    }
}
#pragma warning restore 1591