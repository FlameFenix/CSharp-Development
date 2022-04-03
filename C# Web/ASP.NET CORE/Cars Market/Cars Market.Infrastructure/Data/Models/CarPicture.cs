using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
	public class CarPicture
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		public byte[] Picture { get; set; }

		public Car Car { get; set; }

		[ForeignKey(nameof(Car))]
		public Guid CarId { get; set; }
	}
}
