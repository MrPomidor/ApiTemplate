using System;
using System.Collections.Generic;
using System.Text;

namespace App.Customers.Exceptions
{
     public class CustomerCollisionException:Exception
     {
        public CustomerCollisionException(string message) : base(message) { }
    }
}
