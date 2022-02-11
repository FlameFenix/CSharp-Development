using SMS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Data.Models
{
    public class Cart
    {
        [Key]
        [StringLength(Constants.ID_LENGTH)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}