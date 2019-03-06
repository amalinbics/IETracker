using IETracker.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IETracker.Controllers.api
{
    [Authorize]
    public class HomeController : ApiController
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
      
        public IHttpActionResult Get()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);


            var result = _context.Transactions
                .Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                .GroupBy(t => t.TransactionTypeId)
                .Select(lg => new { Type = lg.Key, Total = lg.Sum(l => l.Amount) });


            decimal[] transactionSum = { 0, 0 };
              

            foreach (var item in result)
            {
                if (item.Type == TransactionType.Income)
                    transactionSum[0] = item.Total;
                else if (item.Type == TransactionType.Expense)
                    transactionSum[1] = item.Total;                      
            }

            return Ok(transactionSum);


        }
       
        public IHttpActionResult GetCategoryWiseInocmeExpense()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            var result = _context.Transactions
                        .Where(t => t.TransactionDate >= fromDate && t.TransactionDate <= toDate)
                        .GroupBy(g => g.CategoryId)
                        .Select(g => new { Category = g.Select(t => t.Category), Total = g.Sum(t => t.Amount) });
         
            var category = result.Select(t => t.Category);
            var amount = result.Select(t => t.Total);

            Dictionary<string, dynamic> finalResult = new Dictionary<string, dynamic>();
            finalResult.Add("category", category);
            finalResult.Add("amount", amount);
            return Ok(result);
        }

        public IHttpActionResult GetBalance()
        {
            var balanceDate = _context.Balances.Max(b => b.BalanceDate);
            var balance = _context.Balances.SingleOrDefault(b => b.BalanceDate == balanceDate);
            return Ok(balance.Amount);           
        }


    }
}
