using Cars_Market.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Models
{
    public class SendMessageFormView
    {

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(DataConstants.MESSAGE_SENDER_EMAIL_MAX_LENGTH,
            MinimumLength = DataConstants.MESSAGE_SENDER_EMAIL_MIN_LENGTH,
            ErrorMessage = "Email must be in range {2} - {1}")]
        public string RecieverEmail { get; set; }

        [Required]
        [StringLength(DataConstants.MESSAGE_TITLE_MAX_LENGTH,
            MinimumLength = DataConstants.MESSAGE_TITLE_MIN_LENGTH,
            ErrorMessage = "{0} must be in range {2} - {1}")]
        public string Title { get; set; }

        [Required]
        [StringLength(DataConstants.MESSAGE_MESSAGE_MAX_LENGTH,
            MinimumLength = DataConstants.MESSAGE_MESSAGE_MIN_LENGTH,
            ErrorMessage = "{0} must be in range {2} - {1}")]
        public string Message { get; set; }
    }
}
