using App.Configuration;
using App.Models;
using App.Payments.Exceptions;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Payments.Repository
{
    public class InMemoryPaymentsRepositoriy: IPaymentsRepository, ITransientDependency
    {
        List<Payment> payments = new List<Payment>
        {
            new Payment(001,"InProcesing"),
            new Payment(002,"Success"),
            new Payment(003,"Success")
        };
        public IEnumerable<Payment> GetPaymentsByStatus(string Status)
        {
            if (Status == null)
            {
                throw new ArgumentNullException();
            }

            if (Status != "InProcesing" && Status != "Success")
            {
                throw new InvalidStatusException("This status is not right! Please, try again.");
            }

            return payments.Where(p => p.Status == Status);
        }
        public List<Payment> CreatePayment(int PaymentNumber, string Status)
        {
            if (Status == null)
            {
                throw new ArgumentNullException();
            }

            if (Status != "InProcesing" && Status != "Success")
            {
                throw new InvalidStatusException("This status is not right! Please, try again.");
            }

            for (int i = 0; i < payments.Count; i++)
            {
                if (PaymentNumber == payments[i].PaymentNumber)
                {
                    throw new NumberAlreadyExists("This payment number is already exists!");
                }
            }

            payments.Add(new Payment(PaymentNumber, Status));
            return payments;
        }
        public List<Payment> GetListPayments()
        {
            if (payments==null)
            {
                throw new EmptyList("List is empty!");
            }

            return payments;
        }
    }
}
