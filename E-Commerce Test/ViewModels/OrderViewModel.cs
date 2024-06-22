using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDetails = new List<OrderDetailsViewModel>();
        }
        public string AppUserId { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        public PaymentDetailsViewModel PaymentDetails { get; set; }
        public List<OrderDetailsViewModel> OrderDetails { get; set; }
        public decimal OrderTotal => OrderDetails?.Sum(x => x.Price * x.Quantity) ?? 0; // Calculated property for OrderTotal

    }
}
