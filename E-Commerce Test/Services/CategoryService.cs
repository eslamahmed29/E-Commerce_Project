using DataAccess.Data;
using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Dtos.CategoriesDtos;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _category;
        public CategoryService(IBaseRepository<Category> category)
        {
            _category = category;
        }
     
        public async Task<IEnumerable<GetCategoryDto>> GetCategories()
        {
          var categories = await _category.Include(c=>c.Products).ToListAsync();
            if(categories == null)
            {
                return null;
            }
            return categories
                .Select(c => new GetCategoryDto
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    Description = c.Description,
                    CreatedDate = c.CreatedDate,
                    ProductCount = c.Products.Count()
                }).OrderBy(c=>c.CategoryName);
                
        }
        public async Task CreateCategory (CreateCategorDto category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                CreatedDate = DateTime.Now,
                Description = category.Description
            };
            await _category.AddAsync(newCategory);
        }

        public async Task UpdateCategory(PostCategoryDto updateCategoryDto)
        {
            var category = await _category.FirstOrDefaultASync(c => c.Id == updateCategoryDto.Id);
            if (category != null)
            {
                category.Name = updateCategoryDto.Name;
                category.Description = updateCategoryDto.Description;

                await _category.UpdateAsync(category);
                
            }
        }

        public async Task DeleteCategory(string Id)
        {
            var category = await _category
                .FirstOrDefaultASync(c => c.Id == Id);
            if (category != null)
            {
                await _category.DeleteAsync(category);
            }
        }

        public async Task<GetCategoryDto?> GetCategory(string Id)
        {
            var category = await _category.Include(c => c.Products).FirstOrDefaultAsync(c=>c.Id == Id);
            if (category == null)
            {
                return null;
            }

                return new  GetCategoryDto
                {
                    Id = category.Id,
                    CategoryName = category.Name,
                    CreatedDate = category.CreatedDate,
                    Description = category.Description,
                    ProductCount = category.Products.Count()
                };
        }

       
    }
}
