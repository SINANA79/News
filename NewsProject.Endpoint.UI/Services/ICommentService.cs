using Microsoft.AspNetCore.WebUtilities;
using NewsProject.Endpoint.UI.Dtos.Comment;
using NewsProject.Endpoint.UI.Dtos.Features;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace NewsProject.Endpoint.UI.Services
{
    public interface ICommentService
    {
        Task<PagingResponse<GetCommentDto>> GetComments(CommentsParameters commentsParameters);
        Task<GetCommentDto> GetComment(Guid id);
        Task<CommentUpdateDto> GetCommentForUpdate(Guid id);
        Task CreateComment(Guid newsId ,CommentCreationDto comment);
        Task UpdateComment(Guid id, CommentUpdateDto comment);
        Task AcceptComment(Guid id);
        Task DeleteComment(Guid id);

    }

    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AcceptComment(Guid id)
        {
            await _httpClient.PutAsync($"/api/Comment/accept/{id}", null);
        }

        public async Task CreateComment(Guid newsId, CommentCreationDto comment)
        {
            await _httpClient.PostAsJsonAsync($"/api/Comment/api/news/{newsId}/comment", comment);
        }

        public async Task DeleteComment(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/Comment/{id}");
        }

        public async Task<PagingResponse<GetCommentDto>> GetComments(CommentsParameters commentsParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = commentsParameters.PageNumber.ToString(),
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/comment", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<GetCommentDto>
            {
                Items = JsonConvert.DeserializeObject<List<GetCommentDto>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
            return pagingResponse;
        }

        public async Task<GetCommentDto> GetComment(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<GetCommentDto>($"/api/Comment/{id}");
        }

        public async Task<CommentUpdateDto> GetCommentForUpdate(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CommentUpdateDto>($"/api/Comment/update/{id}");
        }

        //public async Task<IEnumerable<GetCommentDto>> GetComments()
        //{
        //    return await _httpClient.GetFromJsonAsync<IEnumerable<GetCommentDto>>($"/api/Comment");
        //}

        public async Task UpdateComment(Guid id, CommentUpdateDto comment)
        {
            await _httpClient.PutAsJsonAsync($"/api/Comment/{id}", comment);
        }
    }
}
