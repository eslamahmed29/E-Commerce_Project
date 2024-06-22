using E_Commerce_Test.Models.Dtos.CategoriesDtos;
using E_Commerce_Test.Models.Dtos.ProductsDtos;
using E_Commerce_Test.Models.Models;

namespace E_Commerce_Test.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllProducts();
        Task<GetProductDto> GetProductById(string Id);
        Task CreateProduct(CreateProductDto CreateProductDto,IWebHostEnvironment webHostEnvironment);
        Task UpdateProduct(PostProductDto UpdateProductDto,IWebHostEnvironment webHostEnvironment);
        Task DeleteProduct(string Id,IWebHostEnvironment webHostEnvironment);
        Task<IEnumerable<Category>> GetCategories();
    }
}
