using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using App.Repositories;
using App.Models;
namespace App.Customers.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {

        static List<Customer> customersList = new List<Customer>();
        int Size{ get => customersList.Count; }
        static CustomersRepository()
        {
            customersList.Add(new Customer() { Id = 1, Name = "some1" });
            customersList.Add(new Customer() { Id = 2, Name = "some2" });
            customersList.Add(new Customer() { Id = 3, Name = "some3" });
            customersList.Add(new Customer() { Id = 4, Name = "some4" });
            customersList.Add(new Customer() { Id = 5, Name = "some5" });
            customersList.Add(new Customer() { Id = 6, Name = "some6" });
        }
        public IEnumerable<Customer> GetCustomers()
        { 
            if (Size == 0)
                return null;
            else
            return customersList;
        }

        public Customer GetCustomer(int id)
        {
            return customersList[id];
        }

        public void Add(Customer customer)
        {
            customersList.Add(customer);
        }

        public void Update(Customer newCustomer)
        {
            foreach(Customer item in customersList)
            {
                if (item.Id == newCustomer.Id) {
                    customersList[ customersList.IndexOf(item)] = newCustomer;
                    break;
                }
            }
        }
    }
}
