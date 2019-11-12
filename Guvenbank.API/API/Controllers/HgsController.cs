using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HgsController : ControllerBase
    {
        ICustomerService customerService;
        IBankAccountService bankAccountService;
        public HgsController(ICustomerService customerService, IBankAccountService bankAccountService)
        {
            this.customerService = customerService;
            this.bankAccountService = bankAccountService;
        }

        [HttpGet("{no}", Name = "Get")]
        public IActionResult Get(int no)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5003/");
            HttpResponseMessage response = httpClient.GetAsync("api/account/" + no).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;


            return Ok(responseJson);
        }

        [HttpPost("find")]
        public IActionResult Find([FromBody] TcModel tcModel)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5003/");

            var content = new StringContent(tcModel.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/account/find", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            return Ok(responseJson);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TcModel tcModel)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5003/");

            var content = new StringContent(tcModel.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/account", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            return Ok(responseJson);
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] HgsModel hgsModel)
        {
            Customer customer = customerService.Get(User.Identity.Name);

            BankAccount bankAccount = bankAccountService.Get(hgsModel.BankAccountNo, customer.No);
            bankAccount.Balance -= hgsModel.Balance;

            if (hgsModel.Balance <= 0) return Ok(new { status = "failed", message = "Geçersiz tutar." });
            if (bankAccount.Balance <= 0 || bankAccount.Balance < hgsModel.Balance) return Ok(new { status = "failed", message = "Hesap bakiyesi yetersiz." });

            bankAccountService.Update(bankAccount);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://207.154.196.92:5003/");

            var content = new StringContent(hgsModel.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("api/account/deposit", content).Result;

            string responseBody = response.Content.ReadAsStringAsync().Result;

            JObject responseJson = JsonConvert.DeserializeObject(responseBody) as JObject;

            return Ok(responseJson);
        }
    }
}
