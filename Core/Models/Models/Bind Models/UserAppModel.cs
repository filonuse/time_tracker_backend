using Core.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.Models.Bind_Models
{
    public class UserAppModel : BaseEntity
    {
        public UserAppModel() { }

        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        public string AppId { get; set; }
        public virtual AppInfoModel App { get; set; }

        public bool? IsUseful { get; set; }
    }
}
