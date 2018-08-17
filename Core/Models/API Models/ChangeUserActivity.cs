using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.API_Models
{
    public class ChangeUserActivity
    {
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime StatusChangeTime { get; set; }
    }
}
