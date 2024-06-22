using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Services;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_Test.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IPaymentService _paymentService;
        public OrderController(IOrderService orderService, UserManager<AppUser> userManager, IShoppingCartService shoppingCartService, IPaymentService paymentService)
        {
            _orderService = orderService;;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
            _paymentService = paymentService;
        }
      
        public async Task <IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account", new { Area = "Identity" });
            }

            var orders = await _orderService.GetAllOrdersAsync(userId);
            return View(orders);
        }
        public async Task<IActionResult> Details(string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if(order == null)
            {
                return RedirectToAction("Index");
            }
            var orderDetails = await _orderService.GetOrderDetailsByOrderIdAsync(id);
            var model =  new OrderDetailViewModel
            {
                OrderHeader = order,
                OrderDetails = orderDetails
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync(userId);
            if(!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty";
                return RedirectToAction("Index");
            }
            var orderDetails = cartItems.Select(item => new OrderDetailsViewModel
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
            var model = new OrderViewModel
            {
                OrderDetails = orderDetails,
                ShippingAddress=string.Empty,
                BillingAddress=string.Empty,
                PaymentDetails = new PaymentDetailsViewModel()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
           var userId = _userManager.GetUserId(User);
           if(userId == null)
           {
               return RedirectToAction("Login","Account");
           }
           var orderHeader = await _orderService.CreateOrderAsync(userId,model);
            await _paymentService.ProcessPaymentAsync(orderHeader,model.PaymentDetails);
            await _shoppingCartService.ClearShoppingCartAsync(userId);
            return RedirectToAction("Index");

        }
        
    }
}
