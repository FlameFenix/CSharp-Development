namespace Cars_Market.Models
{
    public class AddSellerFormModel
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Location { get; set; }

        public IFormFile Picture { get; set; }
    }
}
