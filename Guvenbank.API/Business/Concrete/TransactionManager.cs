using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TransactionManager : ITransactionService
    {
        ICustomerDal customerDal;
        IBankAccountDal bankAccountDal;
        ITransactionDal transactionDal;

        public TransactionManager(ICustomerDal customerDal, IBankAccountDal bankAccountDal, ITransactionDal transactionDal)
        {
            this.customerDal = customerDal;
            this.bankAccountDal = bankAccountDal;
            this.transactionDal = transactionDal;
        }

        public (string, string) External(int senderCustomerNo, int senderBankAccountNo, int receiverCustomerNo, int receiverBankAccountNo, decimal amount, string summary)
        {
            Customer senderCustomer = customerDal.Get(x => x.No == senderCustomerNo);
            Customer receiverCustomer = customerDal.Get(x => x.No == receiverCustomerNo);
            BankAccount senderBankAccount = bankAccountDal.Get(x => x.No == senderBankAccountNo && x.CustomerNo == senderCustomerNo);
            BankAccount receiverBankAccount = bankAccountDal.Get(x => x.No == receiverBankAccountNo && x.CustomerNo == receiverCustomerNo);

            if (senderCustomer == null) return ("failed", "Gönderen kullanıcı bulunamadı.");

            if (receiverCustomer == null) return ("failed", "Alıcı kullanıcı bulunamadı.");

            if (senderBankAccount == null) return ("failed", "Gönderici banka hesabı bulunamadı.");

            if (receiverBankAccount == null) return ("failed", "Alıcı banka hesabı bulunamadı.");

            if (senderCustomerNo == receiverCustomerNo) return ("failed", "Kendinize havale yapamazsınız. Lütfen virman olarak deneyin.");

            if (amount <= 0) return ("failed", "Geçersiz tutar.");

            if (senderBankAccount.Balance <= 0 || senderBankAccount.Balance < amount) return ("failed", "Hesap bakiyesi yetersiz.");

            senderBankAccount.Balance -= amount;
            receiverBankAccount.Balance += amount;

            Transaction transaction = new Transaction { Amount = amount, From = senderBankAccountNo, FromCustomerNo = senderCustomerNo, To = receiverBankAccountNo, ToCustomerNo = receiverCustomerNo, Summary = summary, ReceiverFullName = receiverCustomer.Name + " " + receiverCustomer.LastName, Type = TransactionTypes.External, Date = DateTime.Now };

            bankAccountDal.Update(senderBankAccount);
            bankAccountDal.Update(receiverBankAccount);
            transactionDal.Add(transaction);

            return ("success", "Havale işlemi başarıyla gerçekleşti.");
        }

        public (string, string) Internal(int senderCustomerNo, int senderBankAccountNo, int receiverBankAccountNo, decimal amount, string summary)
        {
            Customer senderCustomer = customerDal.Get(x => x.No == senderCustomerNo);
            BankAccount senderBankAccount = bankAccountDal.Get(x => x.No == senderBankAccountNo && x.CustomerNo == senderCustomerNo);
            BankAccount receiverBankAccount = bankAccountDal.Get(x => x.No == receiverBankAccountNo && x.CustomerNo == senderCustomerNo);

            if (senderCustomer == null) return ("failed", "Gönderen kullanıcı bulunamadı.");

            if (senderBankAccount == null) return ("failed", "Gönderici banka hesabı bulunamadı.");

            if (receiverBankAccount == null) return ("failed", "Alıcı banka hesabı bulunamadı.");

            if (senderBankAccountNo == receiverBankAccountNo) return ("failed", "Aynı hesaba para gönderemezsiniz. Lütfen farklı bir hesabınızı seçin.");

            if (amount <= 0) return ("failed", "Geçersiz tutar.");

            if (senderBankAccount.Balance <= 0 || senderBankAccount.Balance < amount) return ("failed", "Hesap bakiyesi yetersiz.");

            senderBankAccount.Balance -= amount;
            receiverBankAccount.Balance += amount;

            Transaction transaction = new Transaction { Amount = amount, From = senderBankAccountNo, FromCustomerNo = senderCustomerNo, To = receiverBankAccountNo, ToCustomerNo = senderCustomerNo , Summary = summary, ReceiverFullName = senderCustomer.Name + " " + senderCustomer.LastName, Type = TransactionTypes.Internal, Date = DateTime.Now };

            bankAccountDal.Update(senderBankAccount);
            bankAccountDal.Update(receiverBankAccount);
            transactionDal.Add(transaction);

            return ("success", "Virman işlemi başarıyla gerçekleşti.");
        }

        public List<Transaction> GetList(int customerNo)
        {
            return transactionDal.GetList(x => x.FromCustomerNo == customerNo || x.ToCustomerNo == customerNo);
        }
    }
}
