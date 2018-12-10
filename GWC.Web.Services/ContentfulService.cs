using AutoMapper;
using Gwc.Common.Interfaces.Services;
using Gwc.Common.Utilities.Extensions;
using GWC.Web.Dtos;
using GWC.Web.Dtos.Contentful;
using GWC.Web.Interfaces.Data;
using GWC.Web.Interfaces.Services;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GWC.Web.Services
{
    public class ContentfulService : IContentfulService
    {
        private IGwcUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ILoggingService _log;
        private HttpClient _client;
        private MediaTypeFormatterCollection formatters;
        private WeblogConfigurationDto configuration;        

        public ContentfulService(IGwcCommonService commonService, IMapper mapper)
        {
            _unitOfWork = commonService.UnitOfWork;
            _mapper = mapper;
            _log = commonService.LogManager.GetDefaultDbLoggingService();
            _client = new HttpClient
            {
                BaseAddress = new System.Uri("https://cdn.contentful.com/spaces/fgj5iptu3uwg/environments/master/")
            };
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            formatters = new MediaTypeFormatterCollection();
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.contentful.delivery.v1+json"));
            configuration = commonService.Configuration;

        }

        public async Task<PostDto> GetPostByUrl(string url)
        {
            try
            {
                var apiUrl = FormatUrl(new BlogPostRequestDto()).Add($"&fields.url={url}");
                var res = await _client.GetAsync(apiUrl);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsAsync<PostDto>(formatters);
            }
            catch (HttpRequestException ex)
            {
                _log.Error(ex, $"An error occurred  {ex.ToString()}");
                throw;
            }
        }

        public async Task<PostDto> GetPostsByTag(BlogPostRequestDto request)
        {
            try
            {
                var url = FormatUrl(request).Add($"&links_to_entry={request.FilterValue}");
                var res = await _client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsAsync<PostDto>(formatters);
            }
            catch (HttpRequestException ex)
            {
                _log.Error(ex, $"An error occurred  {ex.ToString()}");
                throw;
            }
        }       

        public async Task<PostDto> GetPostsByYear(BlogPostRequestDto request)
        {
            try
            {
                var url = FormatUrl(request) +
                    $"&sys.createdAt[gte]={request.FilterValue}-01-01T00:00:00Z" +
                    $"&sys.createdAt[lte]={request.FilterValue}-12-31T23:59:59Z"; 

                var res = await _client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsAsync<PostDto>(formatters);
            }
            catch (HttpRequestException ex)
            {
                _log.Error(ex, $"An error occurred  {ex.ToString()}");
                throw;
            }
        }

        public async Task<PostDto> GetRecentPosts(BlogPostRequestDto request)
        {
            try
            {
                var url = FormatUrl(request); 
                var res = await _client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsAsync<PostDto>(formatters);
            }
            catch (HttpRequestException ex)
            {
                _log.Error(ex, $"An error occurred getting blogs {ex.ToString()}");
                throw;
            }
        }

        public async Task<TagDto> GetTags()
        {
            try
            {
                var url = FormatUrl(new BlogPostRequestDto(contentType: "tag", order:"fields.name"));
                var res = await _client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsAsync<TagDto>(formatters);
            }
            catch (HttpRequestException ex)
            {
                _log.Error(ex, $"An error occurred getting blogs {ex.ToString()}");
                throw;
            }
        }

        private string FormatUrl(BlogPostRequestDto request)
        {
            var queryString = $"{request.Endpoint}?access_token={configuration.ContentfulDeliveryApiKey}" +
                    $"&content_type={request.ContentType}" +
                    $"&select={request.Fields}" +
                    $"&order={request.Order}" +
                    $"&include={request.Include}";

            if (request.CurrentPage > 1 && request.Limit.IsNumeric())
            {
                var postsToSkip = (request.CurrentPage - 1) * request.Limit.ToInt();
                queryString = queryString.Add($"&skip={postsToSkip.ToString()}");                
            }            

            if (request.Limit != "0")
            {
                queryString = queryString.Add($"&limit={request.Limit}");
            }
                                        
            return queryString;
        }
    }
}