using App.Customers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Customers.Filters
{
    public class CustomerExceptionFilter: IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<CustomerExceptionFilter> _logger;
        public CustomerExceptionFilter(ILogger<CustomerExceptionFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case CustomerNotFoundException customerNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not Found: {customerNotFound.EntityType.AssemblyQualifiedName}");
                        break;
                    }
                case CustomerCollisionException customerCollision:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync("Operation with collision");
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
