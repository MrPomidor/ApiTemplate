﻿using App.Configuration;
using App.Stocks.Interfaces;
using App.Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Stocks.Services
{
    public class StocksManager : IStocksManager, ITransientDependency
    {
        private  ICompaniesRepository repository;

        private  IValidateServices validateServices;

        public StocksManager(ICompaniesRepository repository,IValidateServices validateServices)
        {
            this.validateServices = validateServices;
            this.repository = repository;
        }

        public async Task<IEnumerable<StockView>> CompanyStocks(int companyId)
        {

            var company = await Task.Run(() => repository.CompanyById(companyId));

            validateServices.ValidateCompany(company, company?.IsOpenStocks??false);

            List<StockView> stocksView = new List<StockView>();
            
            foreach(var s in company.Stocks)
            {
                stocksView.Add(MappSingleStock(s, company));
            }
            //new List<StocksListItemView>{ };//await Task.Run(() => mapper.Map<IEnumerable<StocksListItemView>>(company.Stocks));


            return stocksView;


        }

        public async Task<StockView> CompanyStockByDate(int companyId, DateTime date)
        {
            var company = await Task.Run(() => repository.CompanyById(companyId));
           
            var stock = await Task.Run(()=>company.Stocks.Where(el => el.CompareDate(date)).FirstOrDefault());

            validateServices.ValidateStocksCompany(stock, company);

            var stockView = MappSingleStock(stock, company);

            return stockView;

        }
        private Stock GetStockByDate(Company company,DateTime Date) => company.Stocks.Where(st => st.CompareDate(Date)).FirstOrDefault();

        private StockView MappSingleStock(Stock stock, Company company) =>
                new StockView
                {
                    Company = company.Name,
                    Cost = stock.Cost,
                    Date = stock.DateView
                };
    }
}

