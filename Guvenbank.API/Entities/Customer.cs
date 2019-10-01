﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public int No { get; set; } //müşteri numarası
        public string IdNo { get; set; } //TCKN
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
