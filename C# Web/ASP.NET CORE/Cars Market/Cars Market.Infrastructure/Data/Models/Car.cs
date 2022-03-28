using Cars_Market.Core.Constants;
using Microsoft.AspNetCore.Http;
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

        [Required]
        [MaxLength(DataConstants.CAR_PICTURE_MAX_VALUE)]
        public byte[] Picture { get; set; }

        public double Rating { get; set; }

        public int VoteCount { get; set; }

        public CarDetails Details { get; set; }

        [ForeignKey(nameof(Seller))]
		public Guid SellerId { get; set; }

		public Seller Seller { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public double RatingResult()
        {
            if(Rating == 0 || VoteCount == 0)
            {
                return 0;
            }

            double result = Rating / (VoteCount * 1.0);
            return result;
        }

	}
}
