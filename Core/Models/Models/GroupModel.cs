using Core.Models.Models.Bind_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Models
{
    public class GroupModel : BaseEntity
    {
        public GroupModel()
        {
            CreationDate = DateTime.Now;
            UserGroups = new List<UserGroupModel>();
        }

        [Required]
        public string GroupName { get; set; }

        public DateTime CreationDate { get; protected set; }
        public DateTime? UpdateDate { get; set; }

        #region Relations
        // Enable one-to-many relation with UserGroupModel.
        public virtual ICollection<UserGroupModel> UserGroups { get; set; }

        // Enable one-to-many relation wuth GroupAppModel.
        public virtual ICollection<GroupAppModel> GroupApps { get; set; }
        #endregion
    }
}
