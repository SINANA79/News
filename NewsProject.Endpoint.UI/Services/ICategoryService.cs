using NewsProject.Endpoint.UI.Dtos.Category;
using System.Net.Http.Json;

namespace NewsProject.Endpoint.UI.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetCategoryById(Guid id);
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task DeleteCategory(Guid id);
        Task CreateCategory(CategoryDto category);
        Task UpdateCategory(Guid id, CategoryDto category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategory(CategoryDto category)
        {
            await _httpClient.PostAsJsonAsync("/api/category/", category);
        }

        public async Task DeleteCategory(Guid id)
        {
            await _httpClient.DeleteAsync($"/api/category/softdelete/{id}");
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("/api/category/");
        }

        public async Task<CategoryDto> GetCategoryById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/category/{id}");
        }

        public async Task UpdateCategory(Guid id, CategoryDto category)
        {
            await _httpClient.PutAsJsonAsync($"/api/category/{id}", category);
        }
    }
}
