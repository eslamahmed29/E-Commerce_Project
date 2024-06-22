using E_Commerce_Test.Interfaces;
using E_Commerce_Test.Models.Models;
using E_Commerce_Test.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; // TODO>
        private readonly SignInManager<AppUser> _signInManager;
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task CreateUserAsync(CreateUserViewModel model, string role )
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                Full_Name = model.Full_Name,
                Address = model.Address,
                City = model.City,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded && !string.IsNullOrEmpty(role))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
                await _userManager.AddToRoleAsync(user, role);
            }

        }

        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
           var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Full_Name= user.Full_Name,
                    Email = user.Email,
                    City= user.City,
                    Address= user.Address,
                    Role = roles.FirstOrDefault(),
                    LockoutEnd = user.LockoutEnd
                };
                userViewModels.Add(userViewModel);
            }

            return userViewModels;
        }

        public async Task<UserProfileViewModel> GetUserProfileAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return null;
            }
            var userProfileViewModel = new UserProfileViewModel
            {
               Id = user.Id,
               FullName = user.Full_Name,
               Email = user.Email,
               City = user.City,
               Address = user.Address,
               PhoneNumber= user.PhoneNumber
            };
            return userProfileViewModel;
        }

        public async Task LockUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(60);
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task UnlockUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return false;
            }
            user.Full_Name = model.FullName;
            user.City = model.City;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return true;
            }
            return false;
        }
    }
}
