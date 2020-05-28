using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
