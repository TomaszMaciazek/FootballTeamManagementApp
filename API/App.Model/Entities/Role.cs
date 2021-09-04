using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Model.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<RoleClaim> Claims { get; set; }
    }
}
