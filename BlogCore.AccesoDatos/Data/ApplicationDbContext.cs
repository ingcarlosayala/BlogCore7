﻿using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Models
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
    }
}