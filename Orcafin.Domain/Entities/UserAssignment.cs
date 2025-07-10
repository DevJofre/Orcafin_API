using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Domain.Entities
{
    public class UserAssignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } // Propriedade de navegação
        public int PlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; } // Propriedade de navegação
        public PaymentMethodType PaymentMethod { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}