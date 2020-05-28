using App.Accounts.Exceptions;
using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts.Repositories
{
	public class InMemoryAccountsRepositoriy : IAccountsRepository, ITransientDependency
	{
		List<Account> account = new List<Account> 
		{ 
			new Account(1,false) ,
			new Account(2,true),
			new Account (3, false)
		};
		public List<Account> GetListAccounts()
		{
			if (account == null)
			{
				throw new ArgumentNullException("List accounts is empty");
			}
			return account;
		}
		public void BlockAccount(int number)
		{
			var item = account.Find(x => x.Number == number);
			if (item == null)
			{
				throw new EntityNotFoundException(typeof(Account));
			}
			if (item.IsBlocked)
			{
				throw new AccountAlreadyBlockedException("You cannot block an already blocked account");
			}
		    item.IsBlocked = true;
		}
		public void UnBlockAccount(int number)
		{
			var item = account.Find(x => x.Number == number);
			if (item == null)
			{
				throw new EntityNotFoundException(typeof(Account));
			}
			if (!item.IsBlocked)
			{
				throw new AccountAlreadyUnblockedException("You cannot Unblock an already Unblocked account");
			}
			item.IsBlocked = false;
		}

	}
}
