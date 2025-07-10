using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Application.Dto
{
    public class PaymentHistoryRequest
    {
        public int UserId { get; set; }
        public DateTime PaidAt { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public int PaymentTypeId { get; set; }
    }
}