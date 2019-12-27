using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;
using App.Customers.Filters;

namespace App.Customers.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [TypeFilter(typeof(CustomerExceptionFilter), Arguments = new object[] { nameof(CustomersController) })]
    public class CustomersController:ControllerBase
    {
        readonly ILogger<CustomersController> _logger;
        readonly ICustomersManager _customersManager;

        public CustomersController(ILogger<CustomersController> logger, ICustomersManager customersManager)
        {
            _logger = logger;
            _customersManager = customersManager;
        }
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            _logger.LogInformation("call Get method");
            var serviceCallResult = _customersManager.GetCustomers().ToList();
            return serviceCallResult;
        }

        [HttpGet("{id}")]
        public Customer GetById( int id)
        {
            _logger.LogInformation("call GetById method");
            var serviceCallResult = _customersManager.GetCustomer(id);
            return serviceCallResult;
        }


        [HttpPost]
        public void Add(Customer customer)
        {
            _logger.LogInformation("call Add method");
            _customersManager.Add(customer);
        }

        [HttpPut]
        public void Update(Customer newCustomer)
        {
            _logger.LogInformation("call Update method");
            _customersManager.Update(newCustomer);
        }
    }
}
