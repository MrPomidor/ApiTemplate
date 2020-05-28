using App.Models;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IPaymentsRepository
    {
        IEnumerable<Payment> GetPaymentsByStatus(string Status);
        List<Payment> CreatePayment(int PaymentNumber, string Status);
        List<Payment> GetListPayments();
    }
}
