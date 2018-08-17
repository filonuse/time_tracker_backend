using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Models
{
    public class LogModel : BaseEntity
    {
        public LogModel() { }

        public DateTime LogTime { get; set; }                   
        public DateTime UsefulTime { get; set; }                
        public DateTime UnusefulTime { get; set; }

        #region Relations
        // Enable many-to-one relation with UserModel.
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        // Enable many-to-one retation with UserWorkSessionDataModel.
        public string SessionId;
        [ForeignKey("SessionId")]
        public virtual UserWorkSessionDataModel WorkSessionData { get; set; }

        // Enable one-to-many relation with FileModel.
        public virtual ICollection<FileModel> Screenshots { get; set; }

        // Enable one-to-many relation with ProcessInfoModel.
        public virtual ICollection<ProcessInfoModel> ApplicationInfo { get; set; }
        #endregion
    }
}       
        
        