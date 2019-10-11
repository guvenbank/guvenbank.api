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

            if (customer == null) return BadRequest(new { status = "failed", message = "TC ya da şifre yanlış." });
            
            return Ok(new { status = "success", FirstName = customer.Name, LastName = customer.LastName, TC = customer.IdNo, PhoneNumber = customer.PhoneNumber, Token = token, No = customer.No } );
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

                int customerNo =  Convert.ToInt32("99" + userModel.TC.Substring(5, 6));

                Customer customer = new Customer { Name = userModel.FirstName, LastName = userModel.LastName, IdNo = userModel.TC, Password = userModel.Password, PhoneNumber = userModel.PhoneNumber, No = customerNo };

                string token = authService.Register(customer);

                if (string.IsNullOrEmpty(token)) return BadRequest(new { status = "failed", message = "Zaten bankamızın müşterisisiniz. Lütfen giriş yapmayı deneyin." });

                userModel.Password = null;

                return Ok(new { status = "success", FirstName = customer.Name, LastName = customer.LastName, TC = customer.IdNo, PhoneNumber = customer.PhoneNumber, Token = token, No = customer.No });
            }
            else return BadRequest();
        }

    }
}
