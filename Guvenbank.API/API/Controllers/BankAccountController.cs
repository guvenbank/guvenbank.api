using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public IEnumerable<BankAccountModel> Get()
        {
            Customer customer = customerService.Get(User.Identity.Name);
        }

        // GET: api/BankAccount/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(Guid id)
        {
            return "value";
        }

        // POST: api/BankAccount
        [HttpPost]
        public void Post([FromBody] BankAccountModel value)
        {
        }

        // PUT: api/BankAccount/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BankAccountModel value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
