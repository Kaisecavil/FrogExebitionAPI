﻿using FrogExhibitionDAL.Models;

namespace FrogExhibitionBLL.Interfaces
{
    public interface IAuthService
    {
        string GenerateTokenString(LoginUser user, IEnumerable<string> roles);
        Task<bool> Login(LoginUser user);
        Task<bool> RegisterUser(LoginUser user);
    }
}