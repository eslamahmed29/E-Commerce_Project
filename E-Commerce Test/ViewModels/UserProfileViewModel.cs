using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test.ViewModels
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
