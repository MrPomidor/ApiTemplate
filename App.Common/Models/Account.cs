using System;

namespace App.Accounts
{
	public class Account
	{
		public Account(int number, bool isBlocked)
		{
			Number = number;
			IsBlocked = isBlocked;
		}

		public int Number { get; set; }
		public bool IsBlocked { get; set; }

	}
}
