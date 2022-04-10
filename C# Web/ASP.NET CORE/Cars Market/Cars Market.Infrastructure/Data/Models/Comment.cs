using Cars_Market.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(DataConstants.COMMENT_AUTHORNAME_MAX_LENGTH)]
        public string? AuthorName { get; set; }

        [Required]
        [MaxLength(DataConstants.COMMENT_PICTURE_SIZE_MAX_LENGHT)]
        public byte[]? AuthorPicture { get; set; }

        [Required]
        [StringLength(DataConstants.COMMENT_TEXT_MAX_LENGTH)]
        public string? Text { get; set; }

        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; }
    }
}
