using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace E_Commerce_Test.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        public AdminController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users()
        {
            var res = await _userService.GetAllUsersAsync();
            return View(res);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LockUser(string Id)
        {
            await _userService.LockUser(Id);
            return RedirectToAction("Users");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlockUser(string Id)
        {
            await _userService.UnlockUser(Id);
            return RedirectToAction("Users");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View(new CreateUserViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _userService.CreateUserAsync(model,"Admin");
                return RedirectToAction("Users");
            }
            // Log the validation errors
           
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UserProfile(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var user = await _userService.GetUserProfileAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UserProfile(UserProfileViewModel model)
        {
            if(ModelState.IsValid)
            {
                var success = await _userService.UpdateUserProfileAsync(model);
                if(success)
                {
                    return RedirectToAction("Index","Customer");
                }
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Orders()
        {
            var res = await _orderService.GetAllOrdersAsync();
            return View(res);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderDetails(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = await _orderService.GetOrderDetailsByOrderIdAsync(id);
            var model = new OrderDetailViewModel
            {
                OrderHeader = order,
                OrderDetails = orderDetails
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus (string orderId, string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return RedirectToAction("Orders");
        }
    }
}
