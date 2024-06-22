using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Models
{
    public class ShoppingCart
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("ProductId")]
        [Required]
        public string ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [Required]
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
        public int Quantity { get; set; }
    }
}
