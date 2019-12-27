using App.Accounts.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Accounts.Controllers
{
	[Route("api/accounts")]
	[ApiController]
	[ServiceFilter(typeof(AccountsExceptionFilter))]
	public class AccountsController : ControllerBase
	{
		readonly ILogger<AccountsController> _logger;
		readonly IAccountsManager _accountsManager;
		public AccountsController(ILogger<AccountsController> logger, IAccountsManager accountsManager)
		{
			_logger = logger;
			_accountsManager = accountsManager;
		}

		// GET api/example/values
		[HttpGet]
		public List<Account> Get()
		{
			_logger.LogInformation("Call Get method");
			var serviceCallResult = _accountsManager.GetListAccounts();
			return serviceCallResult.ToList();
		}
		[Route("/BlockAccount")]
		[HttpPut]
		public void BlockAccount(int number)
		{
			_logger.LogInformation("Call method for block account");
			_accountsManager.BlockAccount(number);
		}
		[Route("/UnBlockAccount")]
		[HttpPut]
		public void UnBlockAccount(int number)
		{
			_logger.LogInformation("Call method for unblock account");
			_accountsManager.UnBlockAccount(number);
		}
	}
}
