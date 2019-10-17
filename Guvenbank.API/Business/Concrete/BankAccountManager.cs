using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BankAccountManager : IBankAccountService
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

        public void Delete(int no, int customerNo)
        {
            BankAccount bankAccount = bankAccountDal.Get(x => x.CustomerNo == customerNo && x.No == no && x.IsActive == true);

            if (bankAccount == null) return;

            bankAccount.IsActive = false;

            Update(bankAccount);
        }

        public BankAccount Get(Guid id)
        {
            return bankAccountDal.Get(x => x.Id == id && x.IsActive == true);
        }

        public BankAccount Get(int no, int customerNo)
        {
            return bankAccountDal.Get(x => x.CustomerNo == customerNo && x.No == no && x.IsActive == true);
        }

        public List<BankAccount> GetList(int customerNo)
        {
            return bankAccountDal.GetList(x => x.CustomerNo == customerNo && x.IsActive == true);
        }

        public bool Update(BankAccount bankAccount)
        {
            bankAccountDal.Update(bankAccount);

            return true;
        }

        public int TotalCount(int customerNo)
        {
            return bankAccountDal.GetList(x => x.CustomerNo == customerNo).Count;
        }
    }
}
