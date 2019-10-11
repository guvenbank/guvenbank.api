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
    public class TransactionController : ControllerBase
    {
        ICustomerService customerService;
        ITransactionService transactionService;

        public TransactionController(ICustomerService customerService, ITransactionService transactionService)
        {
            this.customerService = customerService;
            this.transactionService = transactionService;
        }

        // POST: api/Transaction/External
        [HttpPost("external")]
        public IActionResult External([FromBody] TransactionModel transactionModel)
        {
            Customer senderCustomer = customerService.Get(User.Identity.Name);

            string status = null;
            string message = null;

            (status, message) = transactionService.External(senderCustomer.No, transactionModel.SenderBankAccountNo, transactionModel.ReceiverCustomerNo, transactionModel.ReceiverBankAccountNo, transactionModel.Amount, transactionModel.Summary);

            return Ok( new { status = status, message = message } );
        }

        // POST: api/Transaction/Internal
        [HttpPost("internal")]
        public IActionResult Internal([FromBody] TransactionModel transactionModel)
        {
            Customer senderCustomer = customerService.Get(User.Identity.Name);

            string status = null;
            string message = null;

            (status, message) = transactionService.Internal(senderCustomer.No, transactionModel.SenderBankAccountNo, transactionModel.ReceiverBankAccountNo, transactionModel.Amount, transactionModel.Summary);

            return Ok(new { status = status, message = message });
        }

        // GET: api/Transaction
        [HttpGet]
        public IActionResult Get()
        {
            Customer customer = customerService.Get(User.Identity.Name);

            List<Transaction> transactions = transactionService.GetList(customer.No);

            List<object> transactionObjects = new List<object>();
            foreach (Transaction transaction in transactions)
            {
                transactionObjects.Add( new { senderCustomerNo = transaction.FromCustomerNo, receiverCustomerNo = transaction.ToCustomerNo, senderBankAccountNo = transaction.From, receiverBankAccountNo = transaction.To, amount = transaction.Amount, summary = transaction.Summary, date = transaction.Date, receiverFullName = transaction.ReceiverFullName, type = transaction.Type } );
            }

            return Ok(new { status = "success", transactions = transactionObjects });
        }
    }
}
