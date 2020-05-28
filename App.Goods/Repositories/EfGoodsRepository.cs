using App.Configuration;
using App.Goods.Database;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Goods.Repositories
{
    public class EfGoodsRepository: ITransientDependency, IGodsRepository,IDisposable
    {
        private readonly GoodsDbContext _dbContext;
        public EfGoodsRepository(GoodsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Good GetGood(int id)
        {
            return _dbContext.Goods.Where(g => g.Id == id).FirstOrDefault();
        }

        public IEnumerable<Good> GetGoods()
        {
            var goods = _dbContext.Goods.ToList();
            return goods;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
