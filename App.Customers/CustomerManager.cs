using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using App.Customers;
using App.Repositories;
using App.Models;

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
            return _repository.GetCustomer(id);
        }

        public void Add(Customer customer)
        {
            _repository.Add(customer);
        }
        public void Update( Customer newCustomer)
        { 
           _repository.Update(newCustomer);           
        }
    }
}
