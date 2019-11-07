using System.Collections.Generic;
using App.Models;
namespace App.Repositories
{
    public interface ICustomersRepository
    {
     
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomer(int id);
        void Add(Customer customers);
        void Update(Customer newCustomer);
    }
}
