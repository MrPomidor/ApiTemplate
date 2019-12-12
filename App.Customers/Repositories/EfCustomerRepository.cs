using App.Configuration;
using App.Customers.Database;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Customers.Repositories
{
    public class EfCustomerRepository : ICustomersRepository, IDisposable, ITransientDependency
    {
        private readonly CustomerDbContext _customerDbContext;
        public EfCustomerRepository(CustomerDbContext dbContext)
        {
            _customerDbContext = dbContext;
        }

        public Customer GetCustomer(int id)
        {
            return _customerDbContext.Customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _customerDbContext.Customers.ToList();
            return customers;
        }

        // whn disposing, should dispose related DbContext to prevent memory leak
        public void Dispose()
        {
            _customerDbContext?.Dispose();
        }

        public void Add(Customer customers)
        {
            _customerDbContext.Customers.Add(customers);
            _customerDbContext.SaveChanges();
        }

        public void Update(Customer newCustomer)
        {
            _customerDbContext.Customers.Update(newCustomer);
            _customerDbContext.SaveChanges();
        }
    }
}
