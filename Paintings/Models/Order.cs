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
        //public string UserId { get; set; }
        public int PaintingId { get; set; }
        public List<Painting> paintings { get; set; }
        public int? PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public List<PaintingOrder> PaintingOrder { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
}
