﻿using Cars_Market.Core.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cars_Market.Infrastructure.Data.Models
{
    public class Seller
	{
		[Key]
        public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[EmailAddress]
		[StringLength(DataConstants.SELLER_EMAIL_MAX_LENGTH)]
		public string? Email { get; init; }

		[Required]
		public Profile? Profile { get; set; }

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

		public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
	}
}
