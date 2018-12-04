using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IETracker.DTO
{
    public class TransactionDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

        [Required]
        public byte TransactionTypeId { get; set; }

        public TransactionTypeDto TransactionType { get; set; }
    }
}