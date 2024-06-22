using E_Commerce_Test.Models.Models;

namespace E_Commerce_Test.ViewModels
{
    public class OrderDetailViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}