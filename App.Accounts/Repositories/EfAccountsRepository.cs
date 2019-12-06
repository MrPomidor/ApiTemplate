using App.Accounts.Database;
using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Accounts.Repositories
{
	public class EfAccountsRepository : IAccountsRepository, IDisposable, ITransientDependency
	{
		private readonly AccountDbContext _dbContext;
		public EfAccountsRepository(AccountDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public void BlockAccount(int number)
		{
			var account = _dbContext.Accounts.Where(a => a.Number == number).FirstOrDefault();
			account.IsBlocked = true;
			_dbContext.SaveChanges();
		}

		public List<Account> GetListAccounts()
		{
			return _dbContext.Accounts.ToList();
		}

		public void UnBlockAccount(int number)
		{
			var account = _dbContext.Accounts.Where(a => a.Number == number).FirstOrDefault();
			account.IsBlocked = false;
			_dbContext.SaveChanges();
		}
		public void Dispose()
		{
			_dbContext?.Dispose();
		}
	}
}
