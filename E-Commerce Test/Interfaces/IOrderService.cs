using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;

namespace E_Commerce_Test.Interfaces
{
    public interface IOrderService
    {
        Task <OrderHeader> CreateOrderAsync(string userId,OrderViewModel model);
        Task<OrderHeader> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<OrderHeader>> GetAllOrdersAsync();
        Task<IEnumerable<OrderHeader>> GetAllOrdersAsync(string userId);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderIdAsync(string orderId);
        Task UpdateOrderStatusAsync(string orderId, string status);
        Task<int>GetPendingOrderCountAsync();
    }
}
