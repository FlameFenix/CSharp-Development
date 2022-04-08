using Cars_Market.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class CarDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(DataConstants.CAR_DETAILS_COLOR_MAX_VALUE)]
        public string Color { get; set; }

        [Required]
        [StringLength(DataConstants.CAR_DETAILS_FUELTYPE_MAX_VALUE)]
        public string FuelType { get; set; }

        [StringLength(DataConstants.CAR_DETAILS_DESCRIPTION_MAX_VALUE)]
        public string Description { get; set; }

        [StringLength(DataConstants.CAR_DETAILS_GEARBOXTYPE_MAX_VALUE)]
        public string GearboxType { get; set; }

        public bool IsSold { get; set; }

        public int Visits { get; set; }

        [ForeignKey(nameof(Car))]
        public Guid CarId { get; set; }

        public Car Car { get; set; }

    }
}
