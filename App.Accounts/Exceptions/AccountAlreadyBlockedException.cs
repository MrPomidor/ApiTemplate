using System;
using System.Collections.Generic;
using System.Text;

namespace App.Accounts.Exceptions
{
	public class AccountAlreadyBlockedException : SystemException
	{
		public AccountAlreadyBlockedException(string message) : base(message) { }
	}
}
