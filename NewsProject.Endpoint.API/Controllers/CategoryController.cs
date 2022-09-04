using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Core.Domain.Dtos.Category;
using NewsProject.Core.Domain.Services;

namespace NewsProject.Endpoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var data = _categoryService.GetCategories();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(Guid id)
        {
            var data = _categoryService.GetCategoryById(id);
            return Ok(data);
        }

        [HttpGet("title/{title}")]
        public IActionResult GetCategoryByTitle(string title)
        {
            var data = _categoryService.GetCategoryByTitle(title);
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDto category)
        {
            _categoryService.CreateCategory(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(Guid id, [FromBody] CategoryDto category)
        {
            _categoryService.UpdateCategory(id, category);
            return Ok();
        }

        [HttpDelete("softdelete/{id}")]
        public IActionResult DeleteCategory(Guid id)
        {
            _categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
