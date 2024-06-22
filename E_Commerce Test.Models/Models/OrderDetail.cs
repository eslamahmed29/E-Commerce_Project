using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Models
{
    public class OrderDetail
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public string OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
