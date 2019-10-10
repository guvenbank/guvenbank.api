using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        (Customer, string) Login(string mail, string password);
        string Register(Customer customer);
    }
}
