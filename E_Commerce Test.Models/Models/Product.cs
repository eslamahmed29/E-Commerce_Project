using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Models
{
    public class Product
    {
        [Key]
        public string Id {  get; set; } = new Guid().ToString();
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        [Required]
        public int UnitsInStock { get; set; }
    }
}
