using Microsoft.AspNetCore.Identity;
using Shopier.Application.Dtos.AccountDtos;
using Shopier.Application.Interfaces;
using Shopier.Persistence.Context.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Persistence.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        public UserIdentityRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<string> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);  // kullanıcı var mı
            if (user == null)
            {
                return "User dont exist";
            }
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, true, false);
            if (result.Succeeded)
            {
                return "Login succes";
            }
            if (result.IsLockedOut)
            {
                return "User locked";
            }
            if (result.IsNotAllowed)
            {
                return "Dont have permission";
            }
            if (result.RequiresTwoFactor)
            {
                return "Two factor check required";
            }
            return "Email or Password not true !";
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.RePassword)
            {
                return "Passwords does not match!";
            }

            var user = new AppIdentityUser
            {
                FirstName = dto.Name,
                LastName = dto.Surname,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                UserName = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return "User Created Succesfully!";
            }
            return result.Errors.ToString();
        }
    }
}
