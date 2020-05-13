using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models
{
    public class PaintingOrder
    {
        [Key]
        public int PaintingOrderId { get; set; }
        public int PaintingId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Painting Painting { get; set; }
    }
}
