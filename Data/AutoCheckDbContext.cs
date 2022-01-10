using AutoChek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutoChek.Data
{
    public class AutoCheckDbContext : DbContext
    {
        public AutoCheckDbContext() 
            : base("AutoChekConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

    }
}