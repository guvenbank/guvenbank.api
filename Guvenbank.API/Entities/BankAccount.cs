using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class BankAccount : IEntity
    {
        public Guid Id { get; set; }
        public int No { get; set; } //hesap numarası
        public int CustomerNo { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; } //açılış tarihi
    }
}
