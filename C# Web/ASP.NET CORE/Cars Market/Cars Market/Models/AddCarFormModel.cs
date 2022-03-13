namespace Cars_Market.Models
{
	public class AddCarFormModel
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		public string Make { get; set; }

		public string Model { get; set; }

		public string Year { get; set; }

		public string Money { get; set; }

        public string FuelType { get; set; }

        public string GearboxType { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public IFormFile Image { get; set; }
	}
}
