using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Models
{
    public class AddSellerFormModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Location { get; set; }

        [Required]
        public IFormFile? Image { get; set; }
    }
}
