using System;
using Microsoft.Extensions.Logging;

namespace ND_web.Services
{
    public interface IUserService
    {
        bool IsValidUser(string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        // inject database for user validation
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public bool IsValidUser(string email, string password)
        {
            _logger.LogInformation($"Validating user [{email}]");
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return true;
        }
    }
}
