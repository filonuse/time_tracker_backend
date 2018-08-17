using Core.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Models.API_Models
{
    public class GroupCreationApiModel
    {
        [Required]
        public string GroupName { get; set; }
        public IEnumerable<string> ManagerIDs { get; set; }
        public IEnumerable<string> UserIDs { get; set; }
    }
}