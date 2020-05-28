using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using App.Goods.Exceptions;
using App.Goods.Localization;

namespace App.Goods.Filters
{
    public class GoodExceptionFilter:IAsyncExceptionFilter
    {
        readonly ILogger<GoodExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;
        public GoodExceptionFilter(ILogger<GoodExceptionFilter> logger,
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
                case GoodNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        var errorMessage = _localizationManager.GetResource("GoodsNotFound");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case OrderCreationException orderCreationException:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorMessage = _localizationManager.GetResource("InvalidOrder");
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
            context.ExceptionHandled = true; // this flag should be set to true to stop exception propagation
        }
    }
}
