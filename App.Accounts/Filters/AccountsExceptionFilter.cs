using App.Accounts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Accounts.Filters
{
	public class AccountsExceptionFilter : IAsyncExceptionFilter
	{
		readonly string _context;
		readonly ILogger<AccountsExceptionFilter> _logger;
		public AccountsExceptionFilter(ILogger<AccountsExceptionFilter> logger, string context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			_logger.LogError(context.Exception, $"Error occurred in context of {_context}");
			switch (context.Exception)
			{
				case AccountAlreadyBlockedException accountAlreadyBlocked:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						await context.HttpContext.Response.WriteAsync("Account alredy blocked");
						break;
					}
				case AccountAlreadyUnblockedException accountAlreadyUnblocked:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						await context.HttpContext.Response.WriteAsync("Account already unblocked");
						break;
					}
				case EntityNotFoundException entityNotFound:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.EntityType.AssemblyQualifiedName}");
						break;
					}
				default:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
						await context.HttpContext.Response.WriteAsync("Unhandled exception ! Please, contact support for resolve");
						break;
					}
			}
			context.ExceptionHandled = true; // this flag should be set to true to stop exception propagation
		}
	}
}
