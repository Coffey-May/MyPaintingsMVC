using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models.OrderViewModels
{
    public class OrderLineItem
    {
        public int Id { get; set; }
        public Painting Painting { get; set; }

        public double Cost { get; set; }
    }
}
