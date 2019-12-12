using App.Configuration;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts
{
	public interface IAccountsManager
	{
		List<Account> GetListAccounts();
		void UnBlockAccount(int number);
		void BlockAccount(int number);
	}
	public class AccountsManager : IAccountsManager, ITransientDependency
	{
	
		readonly IAccountsRepository _repository;
		public AccountsManager(IAccountsRepository repository)
		{
			_repository = repository;
		}

		public List<Account> GetListAccounts()
		{
			return _repository.GetListAccounts();
		}
		public void UnBlockAccount(int number)
		{
			_repository.UnBlockAccount(number);
		}
		public void BlockAccount(int number)
		{
			_repository.BlockAccount(number);
		}
	}
}
