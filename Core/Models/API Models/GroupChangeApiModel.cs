using Core.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.API_Models
{
    public class GroupChangeApiModel
    {
        public string GroupName { get; set; }
        public IEnumerable<string> UpdatedManagerIDs { get; set; }
        public IEnumerable<string> UpdatedEmployeeIDs { get; set; }
    }
}
