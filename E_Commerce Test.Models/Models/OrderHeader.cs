using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Models
{
    public class OrderHeader
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public string paymentStatus { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
