using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class CreditApplication : IEntity
    {
        public Guid Id { get; set; }
        public int CustomerNo { get; set; }
        public decimal Salary { get; set; } //müşteri maaşı
        public decimal RequestedAmount { get; set; } //istenen kredi tutarı
        public bool IsWaitingForApproval { get; set; } //başvuru yapıldı, onay veya ret için bekleniyor.
        public bool IsApproved { get; set; } //başvuru onaylandı / reddedildi.
        public decimal ApprovedAmount { get; set; } //onaylanan kredi tutarı
    }
}
