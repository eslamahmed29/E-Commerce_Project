using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Test.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly  UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService;
        public ShoppingCartController(IOrderService orderService,IShoppingCartService shoppingCartService, UserManager<AppUser> userManager)
        {
            _shoppingCartService = shoppingCartService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync(userId);
            var cartTotal = cartItems.Sum(item => item.TotalPrice);
            ViewBag.CartCount = cartItems.Count();
            ViewBag.CartTotal = cartTotal;
            return View(cartItems);
        }
        /*[HttpPost]*/
        /*public async Task<IActionResult> AddToCart(string Id)
        {
            var userId= _userManager.GetUserId(User);
            if(userId == null)
            {
                return RedirectToAction("Login","Account");
            }
            var existingCartItem = (await _shoppingCartService.GetShoppingCartItemsAsync(userId))
                                  .FirstOrDefault(c => c.ProductId == Id);
            if(existingCartItem != null)
            {
                existingCartItem.Quantity++;
                await _shoppingCartService.UpdateShoppingCartItemAsync(existingCartItem);
            }
            else
            {
                var cartItem = new ShoppingCartViewModel
                {
                    ProductId = Id,
                    Quantity = 1,
                    AppUserId = userId,
                };
                await _shoppingCartService.AddShoppingCartItemAsync(cartItem);
            }
            return RedirectToAction("Index","Customer");
            
        }*/
        [HttpPost]
        public async Task<IActionResult> UpdateCart(ShoppingCartViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            
           if(userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
           if(ModelState.IsValid)
            {
                if (model.Quantity < 1)
                {
                    model.Quantity = 1;
                }
                await _shoppingCartService.UpdateShoppingCartItemAsync(model);
                TempData["SuccessMessage"] = "Cart updated successfully";
                return RedirectToAction("Index");
            }
           return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string Id)
        {
           await _shoppingCartService.RemoveFromCartAsync(Id);
            TempData["SuccessMessage"] = "Item removed successfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CartCount()
        {
            var userId = _userManager.GetUserId(User);
            var cartCount = _shoppingCartService.GetCartCount(userId);
            return PartialView("_CartCount",cartCount);
        }
        [Authorize]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
            var userId= _userManager.GetUserId(User);
            var orderHeader = await _orderService.CreateOrderAsync(userId,model);
            await _shoppingCartService.ClearShoppingCartAsync(userId);
            return RedirectToAction("Details", "Order", new { id = orderHeader.Id });
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = _userManager.GetUserId(User);
            await _shoppingCartService.ClearShoppingCartAsync(userId);
            TempData["SuccessMessage"] = "Shopping cart cleared successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
