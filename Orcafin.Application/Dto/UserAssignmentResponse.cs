using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Application.Dto
{
    public class UserAssignmentResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}