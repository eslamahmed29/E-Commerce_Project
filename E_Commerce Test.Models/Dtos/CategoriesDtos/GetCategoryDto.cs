using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.Models.Dtos.CategoriesDtos
{
    public class GetCategoryDto
    {
        public string Id { get; set; } 
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } 
        public int? ProductCount { get; set; }
    }
}
