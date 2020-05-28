using App.Configuration;
using App.Payments.Exceptions;
using App.Payments.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Payments.Filters
{
    public class PaymentsExceptionFilter : IAsyncExceptionFilter,ITransientDependency
    {
        readonly ILogger<PaymentsExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;
        public PaymentsExceptionFilter(
            ILogger<PaymentsExceptionFilter> logger,
            ILocalizationManager localizationManager)
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
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        var errorMessage = _localizationManager.GetResource("Entity Not Found");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case InvalidStatusException invalidStatusException:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorMessage = _localizationManager.GetResource("Invalid status");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case NumberAlreadyExists numberAlreadyExists:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorMessage = _localizationManager.GetResource("Payment number already exists");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                default:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var errorMessage = _localizationManager.GetResource("UnhandeledException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
            }
            context.ExceptionHandled = true;
        }
    }
}

