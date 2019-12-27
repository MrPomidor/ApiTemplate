using System;
using System.Collections.Generic;
using System.Text;

namespace App.Payments.Exceptions
{
    public class NumberAlreadyExists : InvalidOperationException
    {
        public NumberAlreadyExists(string message) : base(message) { }
    }
}
