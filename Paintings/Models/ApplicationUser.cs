﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paintings.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }
     
        public List<Painting> Paintings { get; set; }
       

        public virtual List<Order> Orders { get; set; }
       



    }
}
