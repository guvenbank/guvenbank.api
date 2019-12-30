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
                bankAccountObjects.Add(new { balance = bankAccount.Balance, no = bankAccount.No, createdDate = bankAccount.Date });
            }

            return Ok(new { status = "success", bankAccounts = bankAccountObjects });
        }

        // GET: api/BankAccount/5
        [HttpGet("{no}", Name = "Get")]
        public IActionResult Get(int no)
        {
            Customer customer = customerService.Get(User.Identity.Name);

            BankAccount bankAccount = bankAccountService.Get(no, customer.No);

            if (bankAccount == null) return NotFound();

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance, createdDate = bankAccount.Date });
        }

        // POST: api/BankAccount
        [HttpPost]
        public IActionResult Post()
        {
            Customer customer = customerService.Get(User.Identity.Name);

            int totalCount = bankAccountService.TotalCount(customer.No);
            int bankAccountNo = totalCount + 1001;

            BankAccount bankAccount = new BankAccount();
            bankAccount.CustomerNo = customer.No;
            bankAccount.IsActive = true;
            bankAccount.Balance = 0.0m;
            bankAccount.No = bankAccountNo;
            bankAccount.Date = DateTime.Now;

            bankAccountService.Add(bankAccount);

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance, createdDate = bankAccount.Date });
        }

        // DELETE: api/BankAccount/5
        [HttpDelete("{no}")]
        public IActionResult Delete(int no)
        {
            Customer customer = customerService.Get(User.Identity.Name);

            BankAccount bankAccount = bankAccountService.Get(no, customer.No);
            
            if(bankAccount.Balance > 0) return Ok(new { status = "failed", message = "Lütfen hesabın bakiyesini sıfırlayın." });

            bankAccountService.Delete(no, customer.No);

            return Ok(new { status = "success" });
        }

        // POST: api/BankAccount/Deposit
        [HttpPost("deposit")]
        public IActionResult Deposit(DepositWithdrawModel depositWithdrawModel)
        {
            Customer customer = customerService.Get(User.Identity.Name);

            BankAccount bankAccount = bankAccountService.Get(depositWithdrawModel.No, customer.No);

            if (depositWithdrawModel.Amount <= 0) return Ok(new { status = "failed", message = "Geçersiz tutar." });

            bankAccount.Balance += depositWithdrawModel.Amount;
            bankAccountService.Update(bankAccount);

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance, createdDate = bankAccount.Date });
        }

        // POST: api/BankAccount/Withdraw
        [HttpPost("withdraw")]
        public IActionResult Withdraw(DepositWithdrawModel depositWithdrawModel)
        {
            Customer customer = customerService.Get(User.Identity.Name);

            BankAccount bankAccount = bankAccountService.Get(depositWithdrawModel.No, customer.No);

            if (depositWithdrawModel.Amount <= 0) return Ok(new { status = "failed", message = "Geçersiz tutar." });
            if (bankAccount.Balance <= 0 || bankAccount.Balance < depositWithdrawModel.Amount) return Ok(new { status = "failed", message = "Hesap bakiyesi yetersiz." });

            bankAccount.Balance -= depositWithdrawModel.Amount;
            bankAccountService.Update(bankAccount);

            return Ok(new { status = "success", bankAccount.No, bankAccount.Balance, createdDate = bankAccount.Date });
        }
    }
}
