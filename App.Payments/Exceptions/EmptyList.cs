using System;
using System.Collections.Generic;
using System.Text;

namespace App.Payments.Exceptions
{
    public class EmptyList : SystemException
    {
        public EmptyList(string message) : base(message) { }
    }
}
