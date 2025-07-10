using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Application.Dto
{
    public class UserAssignmentRequest
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}