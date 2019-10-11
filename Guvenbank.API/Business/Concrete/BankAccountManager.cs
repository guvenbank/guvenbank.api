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

        public void Delete(int no)
        {
            BankAccount bankAccount = bankAccountDal.Get(x => x.No == no);
            bankAccount.IsActive = false;

            Update(bankAccount);
        }

        public BankAccount Get(Guid id)
        {
            return bankAccountDal.Get(x => x.Id == id);
        }

        public BankAccount Get(int no)
        {
            return bankAccountDal.Get(x => x.No == no);
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
