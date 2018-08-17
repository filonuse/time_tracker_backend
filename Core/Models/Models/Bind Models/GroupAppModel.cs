using Core.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.Models.Bind_Models
{
    public class GroupAppModel : BaseEntity
    {
        public GroupAppModel() { }

        public string GroupId { get; set; }
        public virtual GroupModel Group { get; set; }

        public string AppId { get; set; }
        public virtual AppInfoModel App { get; set; }
        public bool? IsUseful { get; set; }
    }
}
