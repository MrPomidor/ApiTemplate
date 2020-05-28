using App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Payments.Database
{
    public class PaymentsDbContext : DbContext
    {
        public DbSet<Payment> payment{get; set;}
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Payment>()
                .HasKey(pmt => pmt.PaymentNumber);
        }
    }
}
