using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTraker.Options
{
    public class AuthOptions
    {
        public const string ISSUER = "TimeTrakerServer"; // издатель токена
        public const string AUDIENCE = "TimeTrakerClient"; // потребитель токена
        const string KEY = "TimeTraker_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 20; // время жизни токена (минут)
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
