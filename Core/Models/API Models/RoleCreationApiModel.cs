using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.API_Models
{
    public class RoleCreationApiModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
