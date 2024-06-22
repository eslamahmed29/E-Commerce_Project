using E_Commerce_Test.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Dtos.ProductsDtos
{
    public class GetProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UnitsInStock { get; set; }
    }
}
