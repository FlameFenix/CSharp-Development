using System.ComponentModel.DataAnnotations;

namespace CarsCatalogue.Infrastructure.Data.Models
{
    public class Profile
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public byte[] Picture { get; set; }
    }
}