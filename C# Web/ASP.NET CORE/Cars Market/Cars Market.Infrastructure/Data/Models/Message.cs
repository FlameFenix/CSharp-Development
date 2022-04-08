using Cars_Market.Core.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(DataConstants.MESSAGE_TITLE_MAX_LENGTH)]
        public string Title { get; set; }

        [Required]
        [StringLength(DataConstants.MESSAGE_MESSAGE_MAX_LENGTH)]
        public string Text { get; set; }

        public Guid SellerId { get; set; }

        [StringLength(DataConstants.MESSAGE_RECIEVER_EMAIL_MAX_LENGTH)]
        public string SendToEmail { get; set; }

		[StringLength(DataConstants.MESSAGE_SENDER_EMAIL_MAX_LENGTH)]
        public string SendFromEmail { get; set; }

        public bool IsRead { get; set; }
    }
}