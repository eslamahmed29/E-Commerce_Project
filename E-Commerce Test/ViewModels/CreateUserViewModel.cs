using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.ViewModels
{
    public class CreateUserViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Full_Name { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
