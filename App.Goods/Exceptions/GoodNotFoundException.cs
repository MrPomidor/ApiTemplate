using System;
using System.Collections.Generic;
using System.Text;

namespace App.Goods.Exceptions
{
    public class GoodNotFoundException:Exception
    {
        public Type EntityType { get; private set; }
        public GoodNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
