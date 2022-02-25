using FootballManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class User
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DataConstants.USER_USERNAME_MAX_LENGTH)]
        public string Username { get; init; }

        [Required]
        [StringLength(DataConstants.USER_EMAIL_MAX_LENGTH)]
        public string Email { get; init; }

        [Required]
        [StringLength(DataConstants.USER_PASSWORD_HASHED)]
        public string Password { get; init; }

        public ICollection<UserPlayer> UserPlayers { get; set; } = new List<UserPlayer>();
    }
}
