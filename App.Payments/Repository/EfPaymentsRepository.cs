using App.Configuration;
using App.Models;
using App.Payments.Database;
using App.Payments.Exceptions;
using App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Payments.Repository
{
    public class EfPaymentsRepository : IPaymentsRepository, IDisposable, ITransientDependency
    {
        private readonly PaymentsDbContext _dbContext;
        public EfPaymentsRepository(PaymentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Payment> CreatePayment(int PaymentNumber, string Status)
        {
            var dbPayments = _dbContext.payment;

            if (Status == null)
            {
                throw new ArgumentNullException();
            }

            if (Status != "InProcesing" && Status != "Success")
            {
                throw new InvalidStatusException("Invalid status");
            }
            foreach (var p in dbPayments)
            {
                if (p.PaymentNumber == PaymentNumber)
                {
                    throw new NumberAlreadyExists("Payment number already exists");
                }
            }

            _dbContext.payment.Add(new Payment(PaymentNumber, Status));
            _dbContext.SaveChanges();
            return _dbContext.payment.ToList();
           
        }

        public List<Payment> GetListPayments()
        {
            if(_dbContext.payment==null)
            {
                throw new EmptyList("List is empty!");
            }

            return _dbContext.payment.ToList();
        }

        public IEnumerable<Payment> GetPaymentsByStatus(string Status)
        {
            if (Status == null)
            {
                throw new ArgumentNullException();
            }

            if (Status != "InProcesing" && Status != "Success")
            {
                throw new InvalidStatusException("Invalid status");
            }

            var payment = _dbContext.payment.ToList();
            return payment;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
