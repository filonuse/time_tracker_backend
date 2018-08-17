using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Models
{
    public abstract class BaseEntity
    {
        [Key]
        [MinLength(15)]
        [MaxLength(36)]
        public string Id { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        
    }
}
