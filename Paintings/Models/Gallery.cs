using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models
{
    public class Gallery

    {
        [Required]
        [Key]
        public int GalleryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int PaintingId { get; set; }
        public virtual ICollection<Painting> Paintings { get; set; }

        
    }
}
