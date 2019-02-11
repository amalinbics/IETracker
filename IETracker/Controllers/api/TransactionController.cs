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
        
        [HttpPost]
        [Route("api/transaction/GetTransactionDateRangeWise")]
        public IHttpActionResult GetTransactionDateRangeWise(DateTime fromDate, DateTime toDate)
        {
            var transactions = _context.Transactions
                .Where(t => t.TransactionDate <= fromDate && t.TransactionDate >= toDate)
                .ToList()
                .Select(Mapper.Map<Transaction, TransactionDto>);
            return Ok(transactions);
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
