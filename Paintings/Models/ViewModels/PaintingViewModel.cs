﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models.ViewModels
{
    public class PaintingViewModel
    {
        [Required]
        [Key]
        public int PaintingId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MediumUsed { get; set; }

        public string ImagePath { get; set; }

        public IFormFile File { get; set; }

        public int? GalleryId { get; set; }
        public Gallery Gallery { get; set; }
        public List<SelectListItem> GalleryOptions { get; set; }
        public int Price { get; set; }

        public bool IsSold { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
