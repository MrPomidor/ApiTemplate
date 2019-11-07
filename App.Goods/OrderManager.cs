using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods
{
    public interface IOrderManager
    {
        void AddOrder(Order order);
    }
    public class OrderManager:IOrderManager, ITransientDependency
    {
        readonly IOrderRepository _repository;
        public OrderManager(IOrderRepository repository)
        {
            _repository = repository;
        }
        public void AddOrder(Order order)
        {
            _repository.CreateOrder(order);
        }
    }
}
