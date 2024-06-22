using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Dtos.CategoriesDtos;
using E_Commerce_Test.Models.Dtos.ProductsDtos;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _product;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IBaseRepository<Category> _category;
        public ProductService(IBaseRepository<Product> product, IBaseRepository<Category> category, IWebHostEnvironment hostEnvironment)
        {
            _product = product;
            _category = category;
            _hostEnvironment = hostEnvironment;
        }

        public async Task CreateProduct(CreateProductDto CreateProductDto, IWebHostEnvironment webHostEnvironment)
        {
            var imgurl = await SaveImage(CreateProductDto.Image, webHostEnvironment);
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = CreateProductDto.Name,
                Description = CreateProductDto.Description,
                Price = CreateProductDto.Price,
                ImageUrl = imgurl,
                CategoryId = CreateProductDto.CategoryId,
                UnitsInStock = CreateProductDto.UnitsInStock
            };
            await _product.AddAsync(product);
        }

        public async Task DeleteProduct(string Id, IWebHostEnvironment webHostEnvironment)
        {
            var product = await _product
                .FirstOrDefaultASync(p => p.Id == Id);
            if(product == null)
            {
                
                return;
            }
            DeleteImage(product.ImageUrl,webHostEnvironment);
            await _product.DeleteAsync(product);
        }

        public async Task<IEnumerable<GetProductDto>> GetAllProducts()
        {
            var products = await _product.Include(c => c.Category).ToListAsync();
                if(products == null)
                {
                    return null;
                }
                
                return products
                .Select(p => new GetProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : "No Category" // Check for null
                    ,
                    UnitsInStock = p.UnitsInStock
                }).ToList();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _category.GetAllAsync();
        }

        public async Task<GetProductDto> GetProductById(string Id)
        {
            var product = await _product.Include(c=>c.Category).FirstOrDefaultAsync(p=>p.Id==Id);
               
            if(product == null)
            {
                return null;
            }
            return new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : "No Category" // Check for null
                ,UnitsInStock = product.UnitsInStock
            };
        }

        public async Task UpdateProduct(PostProductDto UpdateProductDto, IWebHostEnvironment webHostEnvironment)
        {
            var product = await _product
                .FirstOrDefaultASync(p => p.Id == UpdateProductDto.Id);
            if(product == null)
            {
                return;
            }
            if (UpdateProductDto.Image != null)
            {
                DeleteImage(product.ImageUrl,webHostEnvironment);
                UpdateProductDto.CurrentImageUrl = await SaveImage(UpdateProductDto.Image,webHostEnvironment);
            }
            product.Name = UpdateProductDto.Name;
            product.Description = UpdateProductDto.Description;
            product.Price = UpdateProductDto.Price;
            product.CategoryId = UpdateProductDto.CategoryId;
            product.ImageUrl = UpdateProductDto.CurrentImageUrl;
            product.UnitsInStock = UpdateProductDto.UnitsInStock;
            await _product.UpdateAsync(product);
        }
        private async Task<string> SaveImage(IFormFile image, IWebHostEnvironment webHostEnvironment)
        {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }
        private void DeleteImage(string imageUrl, IWebHostEnvironment webHostEnvironment)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, imageUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
