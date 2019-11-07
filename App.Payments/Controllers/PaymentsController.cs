using App.Models;
using App.Payments.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Payments.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [TypeFilter(typeof(PaymentsExceptionFilter), Arguments = new object[] { nameof(PaymentsController) })]

    public class PaymentsController : ControllerBase
    {
            readonly ILogger<PaymentsController> _logger;
            readonly IPaymentsManager _paymentsManager;
            public PaymentsController(ILogger<PaymentsController> logger,IPaymentsManager paymentsManager)
            {
            _logger = logger;
            _paymentsManager = paymentsManager;
            }

            [HttpGet] 
            [Route("/getpbystat")]
        public IEnumerable<Payment> GetPaymentsByStatus(string Status)
        {
            _logger.LogInformation("Call method for filtration payments");
            var serviceCallResult = _paymentsManager.GetPaymentsByStatus(Status);
            return serviceCallResult;
        }
        [HttpPost]
        [Route("/createpayment")]
        public ActionResult<List<Payment>> CreatePayment(int PaymentNumber, string Status)
        {
            _logger.LogDebug("Call Create Payment method");
            var serviceCallResult = _paymentsManager.CreatePayment(PaymentNumber, Status);
            return serviceCallResult;
        }
        [HttpGet]
        public ActionResult<List<Payment>> GetListPayments()
        {
            _logger.LogDebug("Call Get List method");
            var serviceCallResult = _paymentsManager.GetListPayments();
            return serviceCallResult;
        }
    }
}
