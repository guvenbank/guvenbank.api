using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public int From { get; set; } //gönderen hesap no
        public int To { get; set; } //alıcı hesap no
        public int FromCustomerNo { get; set; } //gönderen müşteri no
        public int ToCustomerNo { get; set; } //alıcı müşteri no
        public string ReceiverFullName { get; set; } //alıcı adı ve soyadı
        public decimal Amount { get; set; }
        public string Summary { get; set; } //açıklama
        public TransactionTypes Type { get; set; } //havale ya da virman
        public DateTime Date { get; set; }
    }

    public enum TransactionTypes : byte
    {
        Internal, //virman
        External //Havale
    }
}
