using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using App.Goods.Exceptions;

namespace App.Goods.Filters
{
    public class GoodExceptionFilter:IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<GoodExceptionFilter> _logger;
        public GoodExceptionFilter(ILogger<GoodExceptionFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }

         public async Task OnExceptionAsync(ExceptionContext context)
        {

            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case GoodNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.EntityType.AssemblyQualifiedName}");
                        break;
                    }
                case OrderCreationException orderCreationException:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync("Invalid order creation operation");
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
