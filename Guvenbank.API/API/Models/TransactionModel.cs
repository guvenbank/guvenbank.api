using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class TransactionModel
    {
        public int ReceiverCustomerNo { get; set; }

        [Required]
        public int ReceiverBankAccountNo { get; set; }

        [Required]
        public int SenderBankAccountNo { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Summary { get; set; }
    }
}
