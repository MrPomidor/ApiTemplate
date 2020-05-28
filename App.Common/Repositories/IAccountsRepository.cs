using App.Accounts;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IAccountsRepository
    {
		List<Account> GetListAccounts();
		void BlockAccount(int number);
		void UnBlockAccount(int number);

	}
}
