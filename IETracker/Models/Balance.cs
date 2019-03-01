using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IETracker.Models
{
    public class Balance
    {
        public int Id { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal Amount { get; set; }

    }
}