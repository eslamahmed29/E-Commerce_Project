using E_Commerce_Test.Models.Dtos.CategoriesDtos;

namespace E_Commerce_Test.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetCategories();
        Task CreateCategory(CreateCategorDto createCategoryDto);
        Task UpdateCategory(PostCategoryDto updateCategoryDto);
        Task DeleteCategory(string Id);
        Task<GetCategoryDto?> GetCategory(string Id);

    }
}
