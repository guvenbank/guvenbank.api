using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBankAccountService
    {
        void Add(BankAccount bankAccount);

        BankAccount Get(int no, int customerNo);

        BankAccount Get(Guid id);

        void Delete(int no, int customerNo);

        bool Update(BankAccount bankAccount);

        List<BankAccount> GetList(int customerNo);

        int TotalCount(int customerNo);
    }
}
