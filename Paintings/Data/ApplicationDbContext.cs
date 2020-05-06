using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Paintings.Models;

namespace Paintings.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; } 
        public DbSet<Painting> Painting { get; set; }
        public DbSet<Gallery> Gallery { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Painting>().HasData(
        //        new Painting()
        //        {
        //            PaintingId = 1,
        //            Title = "Frog",
        //            MediumUsed = "Oil on Board",
        //            Price = 10000,
        //            ImagePath = "PFrog.jpeg",
        //            GalleryId = 1,
        //            IsSold = true,
        //            ApplicationUserId = user.Id,
                   
        //        }
        //    );
        //}
    }
}
