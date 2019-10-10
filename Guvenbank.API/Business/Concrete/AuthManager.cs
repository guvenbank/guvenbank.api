using Business.Abstract;
using Core.Helpers;
using DataAccess.Abstract;
using Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private ICustomerDal customerDal;
        private readonly AppSettings appSettings;

        public AuthManager(ICustomerDal customerDal, IOptions<AppSettings> appSettings)
        {
            this.customerDal = customerDal;
            this.appSettings = appSettings.Value;
        }

        public (Customer, string) Login(string idNo, string password)
        {
            Customer customer = customerDal.Get(x => x.IdNo == idNo && x.Password == password);

            if (customer == null) return (null, null);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.IdNo)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(createdToken);

            customer.Password = null;

            return (customer, token);
        }

        public string Register(Customer customer)
        {
            Customer tempCustomer = customerDal.Get(x => x.IdNo == customer.IdNo);

            if (tempCustomer != null) return string.Empty; //user exists

            customerDal.Add(customer);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.IdNo)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var createToken = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(createToken);

            return token;
        }
    }
}
