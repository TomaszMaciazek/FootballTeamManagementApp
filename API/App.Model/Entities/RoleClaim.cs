using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace App.Model.Entities
{
    public class RoleClaim
    {
        public Guid Id { get; set; }

        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }
        public Role Role { get; set; }

        public Claim ToClaim() => new(ClaimType, ClaimValue);
    }
}
