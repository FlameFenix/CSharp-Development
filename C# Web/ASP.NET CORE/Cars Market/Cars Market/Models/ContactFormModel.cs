using Cars_Market.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Models
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(DataConstants.CONTACT_FORM_SENDER_EMAIL_MAX_LENGTH,
            MinimumLength = DataConstants.CONTACT_FORM_SENDER_EMAIL_MIN_LENGTH,
            ErrorMessage = "Email should be in range {2} - {1}")]
        public string Sender { get; set; }

        [Required]
        [StringLength(DataConstants.CONTACT_FORM_TITLE_MAX_LENGTH,
            MinimumLength = DataConstants.CONTACT_FORM_TITLE_MIN_LENGTH,
            ErrorMessage = "{0} should be in range {2} - {1}")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(DataConstants.CONTACT_FORM_SENDER_NAME_MAX_LENGTH,
            MinimumLength = DataConstants.CONTACT_FORM_SENDER_NAME_MIN_LENGTH,
            ErrorMessage = "Name should be in range {2} - {1}")]
        public string SenderName { get; set; }

        [Required]
        [StringLength(DataConstants.CONTACT_FORM_MESSAGE_MAX_LENGTH,
            MinimumLength = DataConstants.CONTACT_FORM_MESSAGE_MIN_LENGTH,
            ErrorMessage = "{0} should be in range {2} - {1}")]
        public string Message { get; set; }

    }
}
