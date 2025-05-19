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

        Task<string> IUserIdentityRepository.ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        async Task<string> IUserIdentityRepository.LoginAsync(LoginDto dto)
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
                return "user locked";
            }
            if (result.IsNotAllowed)
            {
                return "Dont have permission";
            }
            if (result.RequiresTwoFactor)
            {
                return "Two factor check required";
            }
            return "Login error";
        }

        async Task IUserIdentityRepository.LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        async Task<string> IUserIdentityRepository.RegisterAsync(RegisterDto dto)
        {
            if (dto.Password != dto.RePassword)
            {
                return "Passwords does not match!";
            }

            var user = new AppIdentityUser{
                   FirstName = dto.Name,
                   LastName = dto.Surname,
                   UserName = dto.Email,
                   Email = dto.Email,
                   PhoneNumber =dto.Phone,
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return "User Created";
            }
            return result.Errors.ToString();
        }
    }
}
