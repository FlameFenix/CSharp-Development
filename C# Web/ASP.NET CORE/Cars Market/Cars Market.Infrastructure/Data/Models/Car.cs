using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCatalogue.Infrastructure.Data.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Year { get; set; }

        [Required]
        public double Money { get; set; }

        [Required]
        [StringLength(20)]
        public string Make { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        public CarDetails Details { get; set; }

        [ForeignKey(nameof(Seller))]
		public Guid SellerId { get; set; }

		public Seller Seller { get; set; }

	}
}
