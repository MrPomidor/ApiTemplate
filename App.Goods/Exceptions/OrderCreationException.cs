using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods.Exceptions
{
    public class OrderCreationException:Exception
    {
        public OrderCreationException(string message):base(message)
        {

        }
    }
}
