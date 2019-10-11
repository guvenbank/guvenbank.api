using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BankAccountModel
    {
        public int No { get; set; }

        public decimal Balance { get; set; }

        public static BankAccount FromModelTo(BankAccountModel bankAccountModel)
        {
            return new BankAccount { No = bankAccountModel.No, Balance = bankAccountModel.Balance };
        }

        public static BankAccountModel FromBankAccountTo(BankAccount bankAccount)
        {
            return new BankAccountModel { No = bankAccount.No, Balance = bankAccount.Balance };
        }
    }
}
