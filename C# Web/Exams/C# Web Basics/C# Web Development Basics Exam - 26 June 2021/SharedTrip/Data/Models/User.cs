using SharedTrip.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
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
        [StringLength(DataConstants.USER_HASH_PASSWORD_MAX_LENGTH)]
        public string Password { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();
    }
}
