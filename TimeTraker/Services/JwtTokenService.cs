using Core.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeTraker.Options;

namespace TimeTraker.Services
{
    public class JwtTokenService
    {
        private readonly UserManager<UserModel> _manager;

        public JwtTokenService(UserManager<UserModel> manager)
        {
            _manager = manager;
        }

        public async Task<string> GetToken(UserModel user)
        {
            IList<Claim> claims = await _manager.GetClaimsAsync(user);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    notBefore: now,
                    expires: now.AddMinutes(AuthOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task RefreshToken() { }

        public async Task DestroyToken() { }
    }
}
