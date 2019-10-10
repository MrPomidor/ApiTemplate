﻿using App.Configuration;
using App.Deposits.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.Deposits.Repositories
{
    public interface IDepositsRepository
    {
        IEnumerable<Deposit> GetAllDeposit();

        Deposit GetDepositById(int id);

        void AddDeposit(Deposit deposit);
    }

    public class InMemmoryDepositsRepository : IDepositsRepository, ITransientDependency
    {
        private static readonly List<Deposit> deposits = Init();


        private static List<Deposit> Init()
        {
            var deposits = new List<Deposit>();
            return null;
        }
        public void AddDeposit(Deposit deposit)
        {
            deposits.Add(deposit);
        }

        public IEnumerable<Deposit> GetAllDeposit()
        {
            return deposits;
        }

        public Deposit GetDepositById(int id) => deposits.Where(x => x.Id == id).FirstOrDefault();
        

    }
}
