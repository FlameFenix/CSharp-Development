using System.ComponentModel.DataAnnotations.Schema;

namespace SharedTrip.Models
{
    public class UserTrip
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string TripId { get; set; }

        [ForeignKey(nameof(TripId))]
        public Trip Trip { get; set; }
    }
}
