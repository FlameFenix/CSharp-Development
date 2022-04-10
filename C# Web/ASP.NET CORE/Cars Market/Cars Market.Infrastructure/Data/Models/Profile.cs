using Cars_Market.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(DataConstants.PROFILE_NAME_MAX_LENGTH)]
        public string? Name { get; set; }

        [Required]
        [StringLength(DataConstants.PROFILE_PHONE_MAX_LENGTH)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(DataConstants.PROFILE_LOCATION_MAX_LENGTH)]
        public string? Location { get; set; }

        [Required]
        [MaxLength(DataConstants.PROFILE_PICTURE_MAX_LENGTH)]
        public byte[]? Picture { get; set; }

        public Guid SellerId { get; set; }

        [ForeignKey(nameof(SellerId))]
        public Seller? Seller { get; set; }
    }
}