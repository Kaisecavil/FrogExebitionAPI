using FrogExebitionAPI.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FrogExebitionAPI.Services
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _context;

        public UserProvider(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserEmail()
        {
            return _context.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.Email).Value;

        }
    }
}
