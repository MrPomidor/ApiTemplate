﻿using App.Configuration;
using App.Stocks.Interfaces;
using App.Stocks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Stocks.Repositories
{
    public class Repository : IRepository, ITransientDependency
    {

        public IQueryable<Company> Companies { get; }

        public Repository()
        {
            Companies = Initializer.InitCompanies();
        }
        public Company CompanyById(int id) => Companies.FirstOrDefault(comp => comp.Id == id && comp.IsAvailableToView);

        public IQueryable<Company> FilteredCompanies(Func<Company, bool> predicate) => Companies.Where(predicate).AsQueryable();


    }
    public static class Initializer
    {
        readonly static string[] companiesName = new string[] { "Amazon", "McDonald’s", "GE", "Samsung", "Apple", "Huawei", "LG", "KFC", "Coca-Cola" };
       
        private static IEnumerable<Stock> GenerateStocks()
        {
            Stock[] stocks = new Stock[7];
            //var a = DateTime.Now.Date;

            for (int i = 0; i < 7; i++)
                            stocks[i] = new Stock { Date = DateTime.Now.AddDays(i*-1), Cost = new Random().Next(200 * i, 400 * i) + 470 * (i+1) };
                
            return stocks;
        }
        public static IQueryable<Company> InitCompanies()
        {
            var companies = new Company[9];
          
                for (int i = 0; i < companies.Length; i++)
                {
                    var stocks =  GenerateStocks();
                    var comp = new Company { Id = i + 1, Name = companiesName[i], Stocks = stocks.ToArray(), Description = "BlaBla", IsAvailableToView = i % 2 == 0 };
                    companies[i] = comp;
                }
            
            return companies.AsQueryable();
        }
    }
}
