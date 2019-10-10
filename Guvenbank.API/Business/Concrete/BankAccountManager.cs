using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BankAccountManager
    {
        IBankAccountDal bankAccountDal;

        public BankAccountManager(IBankAccountDal bankAccountDal)
        {
            this.bankAccountDal = bankAccountDal;
        }

        public void Add(BankAccount bankAccount)
        {
            bankAccountDal.Add(bankAccount);
        }

        public void Delete(Guid id)
        {
            BankAccount bankAccount = Get(id);
            bankAccount.IsActive = false;

            Update(bankAccount);
        }

        public BankAccount Get(Guid id)
        {
            return bankAccountDal.Get(x => x.Id == id);
        }

        public List<BankAccount> GetList(Guid customerId)
        {
            return bankAccountDal.GetList(x => x.Id == customerId);
        }

        public List<BankAccount> GetList()
        {
            return bankAccountDal.GetList();
        }

        public bool Update(BankAccount bankAccount)
        {
            bankAccountDal.Update(bankAccount);

            return true;
        }
    }
}
