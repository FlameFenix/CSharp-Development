using FootballManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class Player
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [StringLength(DataConstants.PLAYER_FULL_NAME_MAX_LENGTH)]
        public string FullName { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DataConstants.PLAYER_POSITION_MAX_LENGTH)]
        public string Position { get; init; }

        [Required]
        [MaxLength(DataConstants.PLAYER_SPEED_MAX_VALUE)]
        public byte Speed { get; init; }

        [Required]
        [MaxLength(DataConstants.PLAYER_SPEED_MAX_VALUE)]
        public byte Endurance { get; init; }

        [Required]
        [StringLength(DataConstants.PLAYER_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; init; }


        public ICollection<UserPlayer> UserPlayers { get; init; } = new List<UserPlayer>();
    }
}
