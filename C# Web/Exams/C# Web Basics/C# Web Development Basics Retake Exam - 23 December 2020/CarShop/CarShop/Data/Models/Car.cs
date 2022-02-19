using CarShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Data.Models
{
    public class Car
    {

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(DataConstants.CAR_MODEL_MAX_LENGTH)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(DataConstants.CAR_IMAGEURL_MAX_LENGTH)]
        public string PictureUrl { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        public int FixedIssues => Issues.Where(x => x.IsFixed == true).Count();

        public int RemainingIssues => Issues.Where(x => x.IsFixed == false).Count();

        [Required]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
