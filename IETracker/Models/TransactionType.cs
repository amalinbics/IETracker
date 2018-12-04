using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IETracker.Models
{
    public class TransactionType
    {
        public byte Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }
    }
}