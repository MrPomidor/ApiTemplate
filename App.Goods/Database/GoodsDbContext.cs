using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods.Database
{
    public class GoodsDbContext: DbContext
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Good>()
                .HasKey(g => g.Id);
            builder.Entity<Order>()
                .HasKey(o=> o.Id);
        }
    }
}
