using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IETracker.DTO
{
    public class TransactionTypeDto
    {
        public byte Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Type { get; set; }
    }
}