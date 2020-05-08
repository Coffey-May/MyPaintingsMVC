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
        public DbSet<Order> Order { get; set; }
        public DbSet<PaintingOrder> PaintingOrder { get; set; }
  
    }
}
