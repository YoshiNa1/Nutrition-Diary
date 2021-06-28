using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ND_web.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        public const string KEY = "This is My 8 secret_ 8 key! 71";   // ключ для шифрации
        public const int LIFETIME = 525600; // время жизни токена - 365 суток
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
