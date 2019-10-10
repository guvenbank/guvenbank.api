using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        void Add(Customer customer);

        Customer Get(Guid id);

        Customer Get(string TC);

        void Delete(Guid id);

        bool Update(Customer customer);
    }
}
