using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Commerce_Test.Controllers
{
    [Authorize (Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, IOrderService orderService, IUserService userService)
        {
            _logger = logger;
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var productCount = _productService.GetAllProducts().Result.Count();
            var orderCount =await _orderService.GetPendingOrderCountAsync();
            var userCount = _userService.GetAllUsersAsync().Result.Count();
            var model = new DashboardViewModel
            {
                PendingOrdersCount = orderCount,
                ProductCount = productCount,
                UserCount = userCount
            };
            return View(model);
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
