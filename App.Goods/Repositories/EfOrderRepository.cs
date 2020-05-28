using App.Configuration;
using App.Goods.Database;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods.Repositories
{
    public class EfOrderRepository : ITransientDependency, IOrderRepository,IDisposable
    {
        private readonly GoodsDbContext _dbContext;
        public EfOrderRepository(GoodsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
