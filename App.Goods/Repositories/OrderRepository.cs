using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods.Repositories
{
    public class OrderRepository:IOrderRepository, ITransientDependency
    {
        List<Order> orders = new List<Order>();
        public void CreateOrder(Order order)
        {
            orders.Add(order);
        }
    }
}
