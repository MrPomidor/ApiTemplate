using App.Configuration;
using App.Models;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using App.Goods.Exceptions;

namespace App.Goods
{
    public interface IGoodsManager
    { 
        IEnumerable<Good> GetGoods();
        Good GetGood(int id);
    }
    public class GoodsManager:IGoodsManager, ITransientDependency
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
            if (_repository.GetGoods().Where(obj => obj.Id == id).FirstOrDefault() == null)
                throw new GoodNotFoundException(typeof(Good));
            return _repository.GetGood(id);
        }

    }
}
