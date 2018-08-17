 using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Models
{
    public class UserWorkSessionDataModel : BaseEntity
    {
        public UserWorkSessionDataModel() { }

        public DateTime StartWork { get; set; }
        public DateTime? StopWork { get; set; }
        public TimeSpan? WorkDuration { get; set; }

        #region Relations
        // Enable many-to-one relation with User model.
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }
        #endregion
    }
}
