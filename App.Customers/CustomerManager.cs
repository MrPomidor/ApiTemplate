using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using App.Configuration;
using App.Customers;
using App.Repositories;
using App.Models;
using App.Customers.Exceptions;

namespace App.Customers
{
    public interface ICustomersManager
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        void Add(Customer customers);
        void Update(Customer newCustomer);
    }
    public class CustomerManager:ICustomersManager
    {
        readonly ICustomersRepository _repository;
        public CustomerManager(ICustomersRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Customer> GetCustomers()
        {

            return _repository.GetCustomers();
        }

        public Customer GetCustomer(int id)
        {
            
            var result = _repository.GetCustomer(id);
            if (result == null)
                throw new CustomerNotFoundException(typeof(Customer));
            return result;
        }

        public void Add(Customer customer)
        {
            if (_repository.GetCustomers().Where(obj => obj.Id == customer.Id).FirstOrDefault() != null)
                throw new CustomerCollisionException("Id Collision of Customers");
            _repository.Add(customer);
        }
        public void Update( Customer newCustomer)
        { 
           _repository.Update(newCustomer);           
        }
    }
}
