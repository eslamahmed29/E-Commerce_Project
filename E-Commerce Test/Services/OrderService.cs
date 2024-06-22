using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.Models.Repositories;
using E_Commerce_Test.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<OrderHeader> _orderHeader;
        private readonly IBaseRepository<OrderDetail> _orderDetail;
        private readonly IBaseRepository<Product> _product;
        private readonly IBaseRepository<ShoppingCart> _shoppingCart;
        public OrderService(IBaseRepository<OrderHeader> orderHeader, IBaseRepository<OrderDetail> orderDetail, IBaseRepository<Product> product, IBaseRepository<ShoppingCart> shoppingCart)
        {
            _orderHeader = orderHeader;
            _orderDetail = orderDetail;
            _product = product;
            _shoppingCart = shoppingCart;
        }
       
        public async Task<OrderHeader> CreateOrderAsync(string userId,OrderViewModel model)
        {
            /* var cartItems = await _shoppingCart.Include(c => c.Product).Include(s=>s.User).Where(s => s.UserId == userId).ToListAsync();
             var orderHeader = new OrderHeader
             {
                 TrackingNumber=Guid.NewGuid().ToString(),
                 AppUserId=userId,
                 OrderDate=DateTime.Now,
                 OrderStatus="Pending",
                 paymentStatus="Pending",
                 OrderTotal=cartItems.Sum(x=>x.Product.Price*x.Quantity),
                BillingAddress=model.BillingAddress,
                ShippingAddress=model.ShippingAddress
             };
             await _orderHeader.AddAsync(orderHeader);
             foreach (var item in cartItems)
             {
                 var orderDetail = new OrderDetail
                 {
                     OrderHeaderId = orderHeader.Id,
                     Price = (double)item.Product.Price,
                     ProductId = item.ProductId,
                     Quantity = item.Quantity
                 };
                 await _orderDetail.AddAsync(orderDetail);

             }
             return orderHeader;*/
            var cartItems = await _shoppingCart
                 .Include(c => c.Product)
                 .Include(c => c.User)
                 .Where(c => c.UserId == userId).ToListAsync();
            var orderHeader = new OrderHeader
            {
                TrackingNumber = Guid.NewGuid().ToString(),
                AppUserId = userId,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                OrderTotal = cartItems.Sum(x => x.Product.Price * x.Quantity),
                BillingAddress = model.BillingAddress,
                ShippingAddress = model.ShippingAddress,
                paymentStatus = "Pending"
            };
            await _orderHeader.AddAsync(orderHeader);
            var orderDetails = cartItems.Select(item => new OrderDetail
            {
                OrderHeaderId = orderHeader.Id,
                Price = (double)item.Product.Price,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList();
            await _orderDetail.AddRangeAsync(orderDetails);
            return orderHeader;
        }
        // For Admin Dashboard
        public async Task<IEnumerable<OrderHeader>> GetAllOrdersAsync()
        {
            return await _orderHeader.Include(c=>c.AppUser).ToListAsync();
        }
        // For Specific User
        public Task<IEnumerable<OrderHeader>> GetAllOrdersAsync(string userId)
        {
            return _orderHeader.WhereAsync(x => x.AppUserId == userId);
        }

        public async Task<OrderHeader> GetOrderByIdAsync(string orderId)
        {
            var res = await _orderHeader
                .Include(c=>c.AppUser)
                .FirstOrDefaultAsync(x => x.Id == orderId);
            return res;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(string orderId)
        {
            var res = await _orderDetail.Include(c=>c.Product).Include(c=>c.OrderHeader.AppUser).Where(x => x.OrderHeaderId == orderId).ToListAsync();
            return res.ToList();
                
        }

        public async Task<int> GetPendingOrderCountAsync()
        {
            return await _orderHeader
                .CountAsync(x=>x.OrderStatus=="Pending");
        }

        public async Task UpdateOrderStatusAsync(string orderId, string status)
        {
            var order = await _orderHeader.GetByIdAsync(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
                await _orderHeader.UpdateAsync(order);
            }
        }
    }
}
