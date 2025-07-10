using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Domain.Entities
{
    public class PaymentHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } // Propriedade de navegação
        public DateTime PaidAt { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; } // Propriedade de navegação
    }
}