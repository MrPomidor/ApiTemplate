using System;
using System.Collections.Generic;
using System.Text;
using App.Configuration;
using App.Models;
using App.Repositories;

namespace App.Goods.Repositories
{
    public class GoodsRepository : IGodsRepository, ITransientDependency
    {
        static List<Good> goodsList;
       
        static GoodsRepository()
        {
            goodsList = new List<Good> { new Good { Id = 1,Name = "Kaptsi", Price = 20 },
           new Good { Id = 2,Name = "Sapogi", Price = 25},
           new Good { Id = 3,Name = "Kufajka", Price = 2}};
        }
        public IEnumerable<Good> GetGoods()
        {
            return goodsList;
        }

        public Good GetGood(int id)
        {
            return goodsList[id];
        }
    }
}
