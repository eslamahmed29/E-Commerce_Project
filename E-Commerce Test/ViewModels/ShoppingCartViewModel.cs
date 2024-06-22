using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.ViewModels
{
    public class ShoppingCartViewModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        [ValidateNever]
        public string ProductName { get; set; }
        [ValidateNever]
        public string ProductImage { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Please enter a value greater than or equal to {1}")]
        public int Quantity { get; set; }
        public string AppUserId { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;
        
    }
}
