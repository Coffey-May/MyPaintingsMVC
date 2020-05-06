using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateTime { get; set; }
        public List<Painting> paintings { get; set; }



        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
