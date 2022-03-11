using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCatalogue.Infrastructure.Data.Models
{
    public class CarDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(20)]
        public string Color { get; set; }

        [StringLength(20)]
        public string FuelType { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(20)]
        public string GearboxType { get; set; }

        public bool IsSold { get; set; }

        public int Visits { get; set; }

        [ForeignKey(nameof(Car))]
        public Guid CarId { get; set; }

        public Car Car { get; set; }

    }
}
