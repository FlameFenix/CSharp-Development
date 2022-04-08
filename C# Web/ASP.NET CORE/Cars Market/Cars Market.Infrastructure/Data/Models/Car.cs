using Cars_Market.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(DataConstants.CAR_YEAR_MIN_VALUE, DataConstants.CAR_YEAR_MAX_VALUE)]
        public int Year { get; set; }

        [Range(DataConstants.CAR_PRICE_MIN_VALUE, DataConstants.CAR_PRICE_MAX_VALUE)]
        public double Money { get; set; }

        [Required]
        [StringLength(DataConstants.CAR_MAKE_MAX_LENGHT)]
        public string Make { get; set; }

        [Required]
        [StringLength(DataConstants.CAR_MODEL_MAX_LENGHT)]
        public string Model { get; set; }

        public byte[] MainPicture { get; set; }
        public ICollection<CarPicture> Pictures { get; set; }

		public bool Approved { get; set; } = false;

        public CarDetails Details { get; set; }

        [ForeignKey(nameof(Seller))]
		public Guid SellerId { get; set; }

		public Seller Seller { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

	}
}
