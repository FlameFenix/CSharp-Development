using SMS.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Models
{
    public class Product
    {
        [Key]
        [StringLength(Constants.ID_LENGTH)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(Constants.PRODUCT_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Range(Constants.PRODUCT_PRICE_MIN_VALUE, Constants.PRODUCT_PRICE_MAX_VALUE)]
        public decimal Price { get; set; }

        public string CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }

    }
}
