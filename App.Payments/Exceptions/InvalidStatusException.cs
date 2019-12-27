using System;
using System.Collections.Generic;
using System.Text;

namespace App.Payments.Exceptions
{
    public class InvalidStatusException: InvalidOperationException
    {
        public InvalidStatusException(string message) : base(message) { }
    }
}
