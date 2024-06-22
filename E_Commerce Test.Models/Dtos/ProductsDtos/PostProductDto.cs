using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Dtos.ProductsDtos
{
    public class PostProductDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public IFormFile? Image { get; set; }
        public string CurrentImageUrl { get; set; }
        public int UnitsInStock { get; set; }
    }
}
