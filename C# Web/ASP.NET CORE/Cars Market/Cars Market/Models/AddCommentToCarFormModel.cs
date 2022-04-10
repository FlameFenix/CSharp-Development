using Cars_Market.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Models
{
    public class AddCommentToCarFormModel
    {
        [Required]
        [RegularExpression(DataConstants.REGEX_FOR_SPECIAL_SYMBOLS)]
        public string? Comment { get; set; }
    }
}
