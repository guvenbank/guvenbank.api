using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public int From { get; set; } //gönderen hesap no
        public int To { get; set; } //alıcı hesap no
        public string ReceiverFullName { get; set; } //alıcı adı ve soyadı
        public int Amount { get; set; }
        public string Summary { get; set; } //açıklama
        public TransactionTypes Type { get; set; }
    }

    public enum TransactionTypes : byte
    {
        Internal, //virman
        EFT
    }
}
