using SMS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Models
{
    public class User
    {
        [Key]
        [StringLength(Constants.ID_LENGTH)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(Constants.USER_USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        [Required]
        [StringLength(Constants.USER_EMAIL_MAX_LENGTH)]
        public string Email { get; set; }

        [Required]
        [StringLength(Constants.USER_PASSWORD_MAX_LENGTH_DB)]
        public string Password { get; set; }

        public string CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
