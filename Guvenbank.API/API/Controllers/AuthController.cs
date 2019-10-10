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
    public class AuthController : ControllerBase
    {

        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.TC) || string.IsNullOrEmpty(loginModel.Password)) return BadRequest();

            loginModel.Password = Core.Helpers.Encryption.Calculate(loginModel.Password);

            Customer customer = null;
            string token = null;

            (customer, token) = authService.Login(loginModel.TC, loginModel.Password);

            if (customer == null) return BadRequest(new { message = "TC or password is incorrect." });

            RegisterModel userModel = new RegisterModel { FirstName = customer.Name, LastName = customer.LastName, TC = customer.IdNo, Token = token, PhoneNumber = customer.PhoneNumber, CustomerNo = customer.No };

            return Ok(userModel);
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Password = Core.Helpers.Encryption.Calculate(userModel.Password);

                userModel.FirstName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.FirstName.Trim().ToLower());

                userModel.LastName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.LastName.Trim().ToLower());

                userModel.CustomerNo =  Convert.ToInt32("3540" + userModel.TC.Substring(5, 6));

                Customer customer = new Customer { Name = userModel.FirstName, LastName = userModel.LastName, IdNo = userModel.TC, Password = userModel.Password, PhoneNumber = userModel.PhoneNumber, No = userModel.CustomerNo };

                string token = authService.Register(customer);

                if (string.IsNullOrEmpty(token)) return BadRequest(new { message = "User already registered." });

                userModel.Token = token;
                userModel.Password = null;

                return Ok(userModel);
            }
            else return BadRequest();
        }

    }
}
