using Cars_Market.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Models
{
	public class AddCarFormModel
	{
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[StringLength(DataConstants.CAR_MAKE_MAX_LENGHT,
			MinimumLength = DataConstants.CAR_MAKE_MIN_LENGHT, 
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string Make { get; set; }

		[Required]
		[StringLength(DataConstants.CAR_MODEL_MAX_LENGHT, 
			MinimumLength = DataConstants.CAR_MODEL_MIN_LENGHT, 
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string Model { get; set; }

		[Required]
		public string Year { get; set; }

		[Required]
		public string Money { get; set; }

		[Required]
		[StringLength(DataConstants.CAR_DETAILS_FUELTYPE_MAX_VALUE, 
			MinimumLength = DataConstants.CAR_DETAILS_FUELTYPE_MIN_VALUE, 
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string FuelType { get; set; }

		[Required]
		[StringLength(DataConstants.CAR_DETAILS_GEARBOXTYPE_MAX_VALUE,
			MinimumLength = DataConstants.CAR_DETAILS_GEARBOXTYPE_MIN_VALUE,
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string GearboxType { get; set; }

		[Required]
		[StringLength(DataConstants.CAR_DETAILS_DESCRIPTION_MAX_VALUE,
			MinimumLength = DataConstants.CAR_DETAILS_DESCRIPTION_MIN_VALUE,
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string Description { get; set; }

		[Required]
		[StringLength(DataConstants.CAR_DETAILS_COLOR_MAX_VALUE,
			MinimumLength = DataConstants.CAR_DETAILS_COLOR_MIN_VALUE,
			ErrorMessage = "{0} should be in range {2} - {1}")]
		public string Color { get; set; }

		[Required]
		public IFormFile Image { get; set; }
	}
}
