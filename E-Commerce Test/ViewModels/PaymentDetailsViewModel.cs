using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.ViewModels
{
    public class PaymentDetailsViewModel
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Expiration must be in MM/YY format", MinimumLength = 5)]
        public string Expiration { get; set; }

        [Required]
        [Range(100, 9999, ErrorMessage = "Invalid CVV")]
        public int CVV { get; set; }

        [Required]
        public string CardHolderName { get; set; }
    }
}
