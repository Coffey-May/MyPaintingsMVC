using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models
{
    public class Painting
    {
        [Required]
        public int PaintingId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MediumUsed { get; set; }

        public string ImagePath { get; set; }
        public int LocationId { get; set; }
        public int Price { get; set; }

        public bool IsSold { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
