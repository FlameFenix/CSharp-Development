using System.ComponentModel.DataAnnotations;

namespace CarsCatalogue.Infrastructure.Data.Models
{
	public class Seller
	{
		[Key]
		public Guid Id { get; init; } = Guid.NewGuid();

		[Required]
		[EmailAddress]
		public string Email { get; init; }

        public Profile Profile { get; set; }

        public Message Message { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

		public ICollection<Message> Messages { get; set; } = new List<Message>();
	}
}
