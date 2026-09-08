using Microsoft.EntityFrameworkCore;
using SecureAuthApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAuthApp.Infrastracture.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Initialize your database context here
        }

        public DbSet<User> Users { get; set; }
    }
}
