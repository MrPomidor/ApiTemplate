using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public IEnumerable<Good> Goods { get; set; }
        public int Count { get; set; }
    }
}
