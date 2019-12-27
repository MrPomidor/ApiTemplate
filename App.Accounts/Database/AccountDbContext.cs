using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts.Database
{
	public class AccountDbContext : DbContext
	{
		public DbSet<Account> Accounts { get; set; }
		public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
		{
			
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Account>()
				.HasKey(acc => acc.Number);
		}
	} 
}
