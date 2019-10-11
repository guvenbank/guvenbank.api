using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBankAccountService
    {
        void Add(BankAccount bankAccount);

        BankAccount Get(int no);

        BankAccount Get(Guid id);

        void Delete(int no);

        bool Update(BankAccount bankAccount);

        List<BankAccount> GetList(int customerNo);

        int TotalCount(int customerNo);
    }
}
