using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITransactionService
    {
        (string, string) External(int senderCustomerNo, int senderBankAccountNo, int receiverCustomerNo, int receiverBankAccountNo, decimal amount, string summary);
        (string, string) Internal(int senderCustomerNo, int senderBankAccountNo, int receiverBankAccountNo, decimal amount, string summary);
        List<Transaction> GetList(int customerTC);
    }
}
