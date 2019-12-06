using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public class Payment
    {
        public Payment(int PNumber, string status)
        {
            PaymentNumber = PNumber;
            Status = status;
        }

        public int PaymentNumber { get; set; }
        public string Status { get; set; }

        public Payment()
        {

        }
    }
}
