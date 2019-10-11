using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            this.customerDal = customerDal;
        }

        public Customer Get(Guid id)
        {
            return customerDal.Get(x => x.Id == id);
        }

        public Customer Get(string TC)
        {
            return customerDal.Get(x => x.IdNo == TC);
        }

        public void Update(Customer customer)
        {
            customerDal.Update(customer);
        }
    }
}
