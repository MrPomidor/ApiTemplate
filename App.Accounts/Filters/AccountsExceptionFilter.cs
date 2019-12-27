using App.Accounts.Exceptions;
using App.Accounts.Localization;
using App.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace App.Accounts.Filters
{
	public class AccountsExceptionFilter : IAsyncExceptionFilter, ITransientDependency
	{
		readonly ILogger<AccountsExceptionFilter> _logger;
		readonly ILocalizationManager _localizationManager;
		public AccountsExceptionFilter(ILogger<AccountsExceptionFilter> logger, ILocalizationManager localizationManager)
		{
			_logger = logger;
			_localizationManager = localizationManager;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			var _context = context.ActionDescriptor.DisplayName;
			_logger.LogError(context.Exception, $"Error occurred in context of {_context}");
			switch (context.Exception)
			{
				case AccountAlreadyBlockedException accountAlreadyBlocked:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						var errorMessage = _localizationManager.GetResource("Account alredy blocked");
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				case AccountAlreadyUnblockedException accountAlreadyUnblocked:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						var errorMessage = _localizationManager.GetResource("Account already unblocked");
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				case EntityNotFoundException entityNotFound:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						var errorMessage = _localizationManager.GetResource("Entity Not Found");
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				default:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						var errorMessage = _localizationManager.GetResource("Unhandled exception");
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
			}
			context.ExceptionHandled = true; // this flag should be set to true to stop exception propagation
		}
	}
}