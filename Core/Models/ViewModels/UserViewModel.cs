using Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(UserModel user)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.PhoneNumber;
            WorkDirection = user.WorkDirection;
            Roles = user.Claims
                .Where(c => c.ClaimType == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                .Select(c => c.ClaimValue);
            Id = user.Id;
            BitrixId = user.BitrixId;
            Create = user.CreationDate;
            Update = user.UpdateDate;
        }

        public string UserName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string WorkDirection { get; }
        public IEnumerable<string> Roles { get; }
        public string Id { get; }
        public int? BitrixId { get; }
        public DateTime Create { get; }
        public DateTime? Update { get; }
    }
}
