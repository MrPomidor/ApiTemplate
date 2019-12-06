using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Customers.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public Type EntityType { get; private set; }
        public CustomerNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
