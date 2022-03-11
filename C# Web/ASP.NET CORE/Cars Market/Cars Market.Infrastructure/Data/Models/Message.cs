using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsCatalogue.Infrastructure.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        [StringLength(100)]
        public string SellerEmail { get; set; }
    }
}