using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks.Models
{
    public class Stock
    {
        public decimal Cost {get;set;}
        public DateTime Date {get;set;}

        public string DateView { 
            get {
                return Date.ToShortDateString();
            } }

        public bool CompareDate(DateTime date) => date.ToShortDateString().CompareTo(DateView) == 0;
        
    }

    public class StocksListItemView
    {
        public decimal Cost { get; set; }

        public string Date { get; set; }

    }
    public class StocksListView
    {
        public IEnumerable<StocksListItemView> Stocks { get; set; }

        public string Company { get; set; }
    }
   
}