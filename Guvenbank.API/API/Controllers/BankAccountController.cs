using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankAccountController : ControllerBase
    {
        ICustomerService customerService;
        IBankAccountService bankAccountService;
        public BankAccountController(ICustomerService customerService, IBankAccountService bankAccountService)
        {
            this.customerService = customerService;
            this.bankAccountService = bankAccountService;
        }

        // GET: api/BankAccount
        [HttpGet]
        public IActionResult Get()
        {
            Customer customer = customerService.Get(User.Identity.Name);

            List<BankAccount> bankAccounts = bankAccountService.GetList(customer.No);

            List<object> bankAccountObjects = new List<object>();
            foreach (BankAccount bankAccount in bankAccounts)
            {
                bankAccountObjects.Add(new { balance = bankAccount.Balance, no = bankAccount.No });
            }

            return Ok(new { status = "success", bankAccounts = bankAccountObjects });
        }

        // GET: api/BankAccount/5
        [HttpGet("{no}", Name = "Get")]
        public IActionResult Get(int no)
        {
            BankAccount bankAccount = bankAccountService.Get(no);

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance });
        }

        // POST: api/BankAccount
        [HttpPost]
        public IActionResult Post()
        {
            Customer customer = customerService.Get(User.Identity.Name);

            int totalCount = bankAccountService.TotalCount(customer.No);
            int bankAccountNo = totalCount + 1;

            BankAccount bankAccount = new BankAccount();
            bankAccount.CustomerNo = customer.No;
            bankAccount.IsActive = true;
            bankAccount.Balance = 0.0m;
            bankAccount.No = bankAccountNo;

            bankAccountService.Add(bankAccount);

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance });
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{no}")]
        public IActionResult Delete(int no)
        {
            bankAccountService.Delete(no);

            return Ok(new { status = "success" });
        }
    }
}
