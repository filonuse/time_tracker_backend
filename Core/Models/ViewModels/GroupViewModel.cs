using Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Models.ViewModels
{
    public class GroupViewModel
    {
        public GroupViewModel(GroupModel group)
        {
            GroupId = group.Id;
            GroupName = group.GroupName;
            Managers = group.UserGroups.Where(ug => ug.IsManager)?.Select(ug => new GroupUserViewModel(ug.User));
            Employees = group.UserGroups.Where(ug => !ug.IsManager)?.Select(ug => new GroupUserViewModel(ug.User));
            Create = group.CreationDate;
            Update = group.UpdateDate;
        }

        public string GroupId { get; }
        public string GroupName { get; }
        public IEnumerable<GroupUserViewModel> Managers { get; }
        public IEnumerable<GroupUserViewModel> Employees { get; }
        public DateTime Create { get; }
        public DateTime? Update { get; }
    }

    public class GroupUserViewModel
    {
        public GroupUserViewModel(UserModel user)
        {
            Id = user.Id;
            BitrixId = user.BitrixId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            WorkDirection = user.WorkDirection;
            Email = user.Email;
        }

        public string Id { get; set; }
        public int? BitrixId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkDirection { get; set; }
        public string Email { get; set; }
    }
}
