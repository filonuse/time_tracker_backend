using Core.Models.Models.Bind_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Models
{
    public class AppInfoModel : BaseEntity
    {
        public AppInfoModel() { }

        public string AppName { get; set; }
        public string AppShortName { get; set; }
        #region Relations
        // Enable one-to-many relation with UserAppModel.
        public virtual ICollection<UserAppModel> UserApps { get; set; }

        // Enable one-to-many relation with GroupAppModel.
        public virtual ICollection<GroupAppModel> GroupApp { get; set; }
        #endregion
    }
}
