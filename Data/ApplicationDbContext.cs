using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductAanbod.Models;

namespace ProductAanbod.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Verzekeraar> Verzekeraar { get; set; }

        public DbSet<Catogorie> Catogorie  { get; set; }

        public DbSet<LaatstAangepast> LaatstAangepast { get; set; }
    }
}
