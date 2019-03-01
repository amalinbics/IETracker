using AutoMapper;
using IETracker.DTO;
using IETracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace IETracker.Controllers.api
{
    [Authorize]
    public class TransactionController : ApiController
    {
        private ApplicationDbContext _context;
        public TransactionController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Get()
        {
            return Ok(_context.Transactions.
                Include(t => t.Category).
                Include(t => t.TransactionType).
                ToList().
                Select(Mapper.Map<Transaction, TransactionDto>));

        }


        public IHttpActionResult Get(int id)
        {
            var transaction = _context.Transactions.
                Include(t => t.Category).
                Include(t => t.TransactionType).
                Single(t => t.Id == id);

            var transactionDto = Mapper.Map<Transaction, TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        [HttpGet]
        [Route("api/transaction/DateRange/{fromdate:datetime}/{todate:datetime}")]
        public IHttpActionResult GetTransactionDateRangeWise(DateTime fromdate, DateTime todate)/*(DateTime fromDate, DateTime toDate)*/
        {

            var transactions = _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.TransactionType)
                .Where(t => t.TransactionDate >= fromdate && t.TransactionDate <= todate)
                .ToList()
                .Select(Mapper.Map<Transaction, TransactionDto>);
            return Ok(transactions);
            // return Ok("test");

        }

        [HttpPost]
        public IHttpActionResult Add(TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var transaction = Mapper.Map<TransactionDto, Transaction>(transactionDto);
            _context.Transactions.Add(transaction);

            if (transaction.TransactionTypeId == TransactionType.Income)
            {
                if (_context.Balances.Where(b => b.BalanceDate == transaction.TransactionDate.Date).Count() == 0)
                {
                    DateTime? lastUpdatedBalanceDate = _context.Balances.Max(b => (DateTime?)b.BalanceDate);
                    lastUpdatedBalanceDate = lastUpdatedBalanceDate == null ? DateTime.Now.Date : lastUpdatedBalanceDate;
                    Balance balance = _context.Balances.SingleOrDefault(a => a.BalanceDate == lastUpdatedBalanceDate);

                    _context.Balances.Add(new Balance { BalanceDate = transaction.TransactionDate.Date, Amount = transaction.Amount + (balance == null ? 0 : balance.Amount) });
                }
                else
                {
                    List<Balance> balances = _context.Balances.Where(b => b.BalanceDate >= transaction.TransactionDate.Date).ToList();
                    foreach (Balance item in balances)
                    {
                        item.Amount += transaction.Amount;
                    }                    
                }
            }

            if (transaction.TransactionTypeId == TransactionType.Expense)
            {
                if (_context.Balances.Where(b => b.BalanceDate == transaction.TransactionDate.Date).Count() == 0)
                {
                    _context.Balances.Add(new Balance { BalanceDate = transaction.TransactionDate, Amount = -transaction.Amount });
                }
                else
                {
                    Balance balance = _context.Balances.Single(b => b.BalanceDate == transaction.TransactionDate.Date);
                    balance.Amount -= transaction.Amount;
                }

            }

            _context.SaveChanges();

            return Created(Request.RequestUri + "/" + transaction.Id, transaction);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var transactionInDb = _context.Transactions.Single(t => t.Id == id);

            Mapper.Map(transactionDto, transactionInDb);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var transactionInDb = _context.Transactions.Single(t => t.Id == id);
            _context.Transactions.Remove(transactionInDb);
            _context.SaveChanges();
            return Ok();
        }


    }
}
