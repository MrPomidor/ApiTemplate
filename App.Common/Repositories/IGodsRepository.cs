using App.Models;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IGodsRepository
    {
        IEnumerable<Good> GetGoods();
        Good GetGood(int id);
        
    }
}
