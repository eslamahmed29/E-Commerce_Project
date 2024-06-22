using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Models.Repositories;
using E_Commerce_Test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Test.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;
        private string userId;
        public CustomerController(UserManager<AppUser> userManager, IShoppingCartService shoppingCartService, IProductService productService)
        {
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
            _productService= productService;
        }
        
        public async Task< IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            ViewBag.CartCount = userId != null ? (await _shoppingCartService.GetShoppingCartItemsAsync(userId)).Count : 0;
            return View(products);
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(string Id)
        {
            var userId = _userManager.GetUserId(User);
            if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            await _shoppingCartService.AddShoppingCartItemAsync(Id, userId);
            TempData["SuccessMessage"] = "Item added to cart Successfully";
            return RedirectToAction("Index", "Customer");
        }
    }
}
