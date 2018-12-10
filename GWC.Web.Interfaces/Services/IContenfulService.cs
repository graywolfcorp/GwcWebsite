using GWC.Web.Dtos.Contentful;
using System.Threading.Tasks;

namespace GWC.Web.Interfaces.Services
{
    public interface IContentfulService
    {
        Task<PostDto> GetPostsByTag(BlogPostRequestDto request);
        Task<PostDto> GetRecentPosts(BlogPostRequestDto request);
        Task<TagDto> GetTags();
        Task<PostDto> GetPostByUrl(string url);
        Task<PostDto> GetPostsByYear(BlogPostRequestDto request);
    }
}