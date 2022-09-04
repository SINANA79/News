using Microsoft.AspNetCore.WebUtilities;
using NewsProject.Endpoint.UI.Dtos;
using NewsProject.Endpoint.UI.Dtos.Features;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace NewsProject.Endpoint.UI.Services
{
    public interface INewsService
    {
        Task<IEnumerable<SliderDto>> GetSlider();
        Task<IEnumerable<NewsCardDto>> GetLatestNews();
        Task<PagingResponse<NewsCardDto>> GetCategoryNewses(Guid categoryId, LatestNewsParameters newsParameters);
        Task<IEnumerable<NewsCardDto>> GetMostViewNews();
        Task<IEnumerable<NewsWithCommentDto>> GetMostCommentNews();
        Task<PagingResponse<NewsWithCategoryDto>> GetAllNewses(NewsParameters newsParameters);
        Task<PagingResponse<NewsCardDto>> GetLatestNewses(LatestNewsParameters newsParameters);
        Task<NewsCreationDto> GetNews(Guid id);
        Task<NewsWithDependentsDto> GetNewsWithDependents(Guid id);
        Task CreateNews(NewsCreationDto news);
        Task<string> UploadNewsImage(MultipartFormDataContent content);
        Task UpdateNews(Guid id, NewsCreationDto news);
        Task DeleteNews(Guid id);

    }

    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateNews(NewsCreationDto news)
        {
            await _httpClient.PostAsJsonAsync("/api/news/", news);
        }

        public async Task DeleteNews(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/news/softdelete/{id}");
        }

        //public async Task<IEnumerable<NewsCreationDto>> GetAllNewses(int pageNumber, int pageSize)
        //{
        //    return await _httpClient.GetFromJsonAsync<IEnumerable<NewsCreationDto>>($"/api/news?PageNumber={pageNumber}&PageSize={pageSize}");
        //}

        public async Task<PagingResponse<NewsWithCategoryDto>> GetAllNewses(NewsParameters newsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = newsParameters.PageNumber.ToString(),
                ["searchTerm"] = newsParameters.SearchTerm == null ? "" : newsParameters.SearchTerm,
                ["orderBy"] = newsParameters.OrderBy == null ? "" : newsParameters.OrderBy
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/news", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<NewsWithCategoryDto>
            {
                Items = JsonConvert.DeserializeObject<List<NewsWithCategoryDto>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<NewsCardDto>> GetLatestNewses(LatestNewsParameters newsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = newsParameters.PageNumber.ToString(),
                ["searchTerm"] = newsParameters.SearchTerm == null ? "" : newsParameters.SearchTerm,
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/news/alllatest", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<NewsCardDto>
            {
                Items = JsonConvert.DeserializeObject<List<NewsCardDto>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<IEnumerable<NewsCardDto>> GetLatestNews()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NewsCardDto>>("/api/news/latest");
        }

        public async Task<NewsCreationDto> GetNews(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<NewsCreationDto>($"/api/news/{id}");
        }

        public async Task<NewsWithDependentsDto> GetNewsWithDependents(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<NewsWithDependentsDto>($"/api/news/dependents/{id}");
        }

        public async Task<IEnumerable<SliderDto>> GetSlider()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<SliderDto>>("/api/news/important");
        }

        public async Task<string> UploadNewsImage(MultipartFormDataContent content)
        {
            var postResult = await _httpClient.PostAsync("/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5001/", postContent);
                return imgUrl;
            }
        }

        public async Task UpdateNews(Guid id, NewsCreationDto news)
        {
            await _httpClient.PostAsJsonAsync($"/api/news/{id}", news);
        }

        public async Task<IEnumerable<NewsCardDto>> GetMostViewNews()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NewsCardDto>>("/api/news/mostview");
        }

        public async Task<IEnumerable<NewsWithCommentDto>> GetMostCommentNews()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NewsWithCommentDto>>("/api/news/mostcomment");
        }

        public async Task<PagingResponse<NewsCardDto>> GetCategoryNewses(Guid categoryId, LatestNewsParameters newsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = newsParameters.PageNumber.ToString(),
                ["searchTerm"] = newsParameters.SearchTerm == null ? "" : newsParameters.SearchTerm,
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"api/category/{categoryId}/newses", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<NewsCardDto>
            {
                Items = JsonConvert.DeserializeObject<List<NewsCardDto>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }
    }
}
