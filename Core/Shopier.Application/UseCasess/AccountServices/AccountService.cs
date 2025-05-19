using Shopier.Application.Dtos.AccountDtos;
using Shopier.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.UseCasess.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IUserIdentityRepository _userIdentityRepository;

        public AccountService(IUserIdentityRepository userIdentityRepository)
        {
            _userIdentityRepository = userIdentityRepository;
        }

        Task<string> IAccountService.ChangePassword()
        {
            throw new NotImplementedException();
        }

        async Task<string> IAccountService.Login(LoginDto dto)
        {
            var result = await _userIdentityRepository.LoginAsync(dto);
            return result;
        }

        async Task IAccountService.Logout()
        {
            await _userIdentityRepository.LogoutAsync();
        }

        async Task<string> IAccountService.Register(RegisterDto dto)
        {
            var result = await _userIdentityRepository.RegisterAsync(dto);
            return result;
        }
    }
}
