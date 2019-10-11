using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Customer Get(Guid id);

        Customer Get(string TC);

        void Update(Customer customer);
    }
}
