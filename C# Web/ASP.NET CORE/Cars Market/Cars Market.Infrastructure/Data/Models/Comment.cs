using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(50)]
        public string AuthorName { get; set; }

        public byte[] AuthorPicture { get; set; }

        [StringLength(256)]
        public string Text { get; set; }

        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }
    }
}
