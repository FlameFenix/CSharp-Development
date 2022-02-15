using SharedTrip.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedTrip.Models
{
    public class Trip
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [MaxLength(DataConstants.TRIP_SEATS_MAX_VALUE)]
        public int Seats { get; set; }

        [Required]
        [StringLength(DataConstants.TRIP_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();
    }
}
