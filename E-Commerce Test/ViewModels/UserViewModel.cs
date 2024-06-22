namespace E_Commerce_Test.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Full_Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
