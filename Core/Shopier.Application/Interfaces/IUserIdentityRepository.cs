﻿using Shopier.Application.Dtos.AccountDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Interfaces
{
    public interface IUserIdentityRepository
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> ChangePasswordAsync();
        Task LogoutAsync();

    }
}
