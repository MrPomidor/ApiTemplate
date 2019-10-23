﻿using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods
{
    public interface IGoodsManager
    { 
        IEnumerable<Good> GetGoods();
        Good GetGood(int id);
        void AddOrder(Order order);
    }
    public class GoodsManager:IGoodsManager
    {
        readonly IGodsRepository _repository;
        public GoodsManager(IGodsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Good> GetGoods()
        {
            return _repository.GetGoods();
        }

        public Good GetGood(int id)
        {
            return _repository.GetGood(id);
        }

        public void AddOrder(Order order)
        {
            _repository.CreateOrder(order);
        }

    }
}
