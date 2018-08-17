using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.API_Models
{
    public class UserChangeApiModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkDirection { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
