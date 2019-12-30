using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/Customer/CreditNumber
        [HttpGet(Name = "CreditNumber")]
        public IActionResult Get()
        {
            Customer customer = customerService.Get(User.Identity.Name);

            return Ok(new { status = "success", creditNumber = customer.CreditNumber });
        }

        // GET: api/Customer/CreditNumber
        [HttpPost(Name = "CreditNumber")]
        public IActionResult Post()
        {
            Customer customer = customerService.Get(User.Identity.Name);
            customer.CreditNumber++;
            customerService.Update(customer);

            return Ok(new { status = "success", creditNumber = customer.CreditNumber });
        }
    }
}