using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Dtos.ProductsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_Test.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService , IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
           await PopulateCategoriesDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            if(ModelState.IsValid)
            {
                await _productService.CreateProduct(createProductDto,_webHostEnvironment);
                return RedirectToAction("Index");
            }
            await PopulateCategoriesDropDownList();

            return View(createProductDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var product = await _productService.GetProductById(Id);
            if(product == null)
            {
                return NotFound();
            }
            await PopulateCategoriesDropDownList();
            var productDto = new PostProductDto
            {
                Id = Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CurrentImageUrl = product.ImageUrl,
                
            };
            return View(productDto);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostProductDto postProductDto)
        {
            if(ModelState.IsValid)
            {
                await _productService.UpdateProduct(postProductDto,_webHostEnvironment);
                return RedirectToAction("Index");
            }
            await PopulateCategoriesDropDownList();
            return View(postProductDto);
        }
        public async Task<IActionResult> Details(string Id)
        {
            var product = await _productService.GetProductById(Id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> Delete(string Id)
        {
            var product = await _productService.GetProductById(Id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string Id)
        {
            await _productService.DeleteProduct(Id, _webHostEnvironment);
            return RedirectToAction("Index");
        }
        private async Task PopulateCategoriesDropDownList()
        {
            var categories = await _productService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}
