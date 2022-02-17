using CarShop.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DataConstants.USER_USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(DataConstants.USER_PASSWORD_MAX_LENGTH_HASHED)]
        public string Password { get; set; }

        public bool IsMechanic { get; set; }
    }
}
