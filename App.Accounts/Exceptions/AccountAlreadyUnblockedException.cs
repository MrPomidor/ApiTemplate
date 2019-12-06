using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts.Exceptions
{
	public class AccountAlreadyUnblockedException : SystemException
	{
		public AccountAlreadyUnblockedException(string message) : base(message) { }
	}
}
