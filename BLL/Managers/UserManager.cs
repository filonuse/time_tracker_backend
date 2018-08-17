using Core.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Managers
{
    public class UserManager : UserManager<UserModel>
    {
        public UserManager(
            IUserStore<UserModel> store, 
            IOptions<IdentityOptions> optionsAccessor, 
            IPasswordHasher<UserModel> passwordHasher, 
            IEnumerable<IUserValidator<UserModel>> userValidators, 
            IEnumerable<IPasswordValidator<UserModel>> passwordValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            IServiceProvider services, ILogger<UserManager<UserModel>> logger) : 
            base(
                store, 
                optionsAccessor, 
                passwordHasher, 
                userValidators, 
                passwordValidators, 
                keyNormalizer, 
                errors, 
                services, 
                logger)
        {
        }
    }
}
