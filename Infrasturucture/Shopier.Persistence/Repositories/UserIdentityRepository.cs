using Shopier.Application.Dtos.AccountDtos;
using Shopier.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Persistence.Repositories
{
    public class UserIdentityRepository : IUserIdentityRepository
    {
        Task<string> IUserIdentityRepository.ChangePassword()
        {
            throw new NotImplementedException();
        }

        Task<string> IUserIdentityRepository.LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        Task IUserIdentityRepository.Logout()
        {
            throw new NotImplementedException();
        }

        Task<string> IUserIdentityRepository.RegisterAsync(RegisterDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
