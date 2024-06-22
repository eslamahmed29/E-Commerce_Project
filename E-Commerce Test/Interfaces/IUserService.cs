using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;

namespace E_Commerce_Test.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllUsersAsync(); // <>
        Task LockUser(string Id);
        Task UnlockUser(string Id);
        Task CreateUserAsync(CreateUserViewModel model,string role);
        Task<UserProfileViewModel> GetUserProfileAsync(string Id); // <>
        Task<bool> UpdateUserProfileAsync(UserProfileViewModel model);
    }
}
