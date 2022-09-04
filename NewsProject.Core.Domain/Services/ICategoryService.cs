using NewsProject.Core.Domain.Dtos.Category;
using NewsProject.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryById(Guid id);
        Category GetCategoryByTitle(string title);
        void CreateCategory(CategoryDto category);
        void UpdateCategory(Guid id, CategoryDto category);
        void DeleteCategory(Guid id);
    }
}
