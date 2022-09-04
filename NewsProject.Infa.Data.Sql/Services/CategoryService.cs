using NewsProject.Core.Domain.Dtos.Category;
using NewsProject.Core.Domain.Entities;
using NewsProject.Core.Domain.Services;
using NewsProject.Infa.Data.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Infa.Data.Sql.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NewsDbContext _context;

        public CategoryService(NewsDbContext context)
        {
            _context = context;
        }

        public void CreateCategory(CategoryDto category)
        {
            Category result = new Category
            {
                CategoryId = Guid.NewGuid(),
                Title = category.Title,
                Description = category.Description,
                IsDeleted = category.IsDeleted
            };
            bool duplicate = DuplicateCategory(category.Title);
            if (duplicate)
                throw new Exception($"{category.Title} exists in database");
            else
            {
                _context.Categories.Add(result);
                _context.SaveChanges();
            }
        }

        public void DeleteCategory(Guid id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            category.IsDeleted = true;
            _context.Categories.Update(category);
            _context.SaveChanges();

        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = _context.Categories.Select(c=> new Category
            {
                CategoryId = c.CategoryId,
                Title = c.Title,
                Description = c.Description,
                IsDeleted = c.IsDeleted
            }).Where(c=>c.IsDeleted==false);
            return categories;
        }

        public Category GetCategoryById(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(c=>c.CategoryId == id);
            Category result = new Category
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Description = category.Description,
                IsDeleted = category.IsDeleted
            };
            return result;
        }

        public Category GetCategoryByTitle(string title)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Title == title);
            Category result = new Category
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Description = category.Description,
                IsDeleted = category.IsDeleted
            };
            return result;
        }

        public void UpdateCategory(Guid id, CategoryDto category)
        {
            var findCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (findCategory == null)
                throw new Exception("Category not found");
            bool duplicate = DuplicateCategory(category.Title);
            findCategory.Title = category.Title;
            findCategory.Description = category.Description;
            findCategory.IsDeleted = category.IsDeleted;
            _context.Categories.Update(findCategory);
            _context.SaveChanges();


        }

        private bool DuplicateCategory(string title)
        {
            return _context.Categories.Where(c => c.Title == title).Any();
        }
    }
}
