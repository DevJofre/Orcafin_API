using Orcafin.Domain.Enums;

namespace Orcafin.Application.Dto
{
    public class PaymentTypeRequest
    {
        public string Name { get; set; }
        public PaymentStatus Status { get; set; }
    }
}