using Core.Models.API_Models;
using Core.Models.Models.Bind_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Models
{
    public class UserModel : IdentityUser
    {
        public UserModel()
        {
            CreationDate = DateTime.Now;
            UserGroups = new List<UserGroupModel>();
            WorkSessionData = new List<UserWorkSessionDataModel>();
            Logs = new List<LogModel>();
            Claims = new List<IdentityUserClaim<string>>();
        }
        public UserModel(UserCreationApiModel user):
            this()
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            WorkDirection = user.WorkDirection;
        }

        /// <summary>
        /// Id to connect user to Bitrix service.
        /// </summary>
        public int? BitrixId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string WorkDirection { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreationDate { get; private set; }
        public DateTime? UpdateDate { get; set; }

        #region Relations
        // Enable one-to-many relation with UserGroupModel.
        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        // Enable one-to-many relation with UserWorkTimeDataModel.
        public virtual ICollection<UserWorkSessionDataModel> WorkSessionData { get; set; }

        // Enable one-to-many relation with LogModel.
        public virtual ICollection<LogModel> Logs { get; set; }

        // Enable one-to-many relation with IdentityUserRole model.
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; }

        // Enable one-to-many relation with UserAppModel.
        public virtual ICollection<UserAppModel> UserApps { get; set; }
        #endregion
    }
}
