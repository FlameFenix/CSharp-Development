using CarShop.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Data.Models
{
    public class Issue
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DataConstants.ISSUE_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        public bool IsFixed { get; set; }

        [Required]
        public string CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }
    }
}