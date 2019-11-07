using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Models;

namespace App.Customers.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController:ControllerBase
    {
       
        readonly ICustomersManager _customersManager;

        public CustomersController(ICustomersManager customersManager)
        {
           
            _customersManager = customersManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
           
            if (_customersManager.GetCustomers() == null)
                return NotFound();
            else
            {
                var serviceCallResult = _customersManager.GetCustomers().ToList();
                return serviceCallResult;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById( int id)
        {
          
            var serviceCallResult = _customersManager.GetCustomer(id);
            return serviceCallResult;
        }


        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            
            _customersManager.Add(customer);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update(Customer newCustomer)
        {

            _customersManager.Update(newCustomer);
            return Ok();
            
        }
    }
}
