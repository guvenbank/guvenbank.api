using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBankAccountService
    {
        void Add(BankAccount bankAccount);

        BankAccount Get(Guid id);

        void Delete(Guid id);

        bool Update(BankAccount bankAccount);

        List<BankAccount> GetList(Guid customerId);
    }
}
