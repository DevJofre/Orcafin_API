using Orcafin.Domain.Enums;

namespace Orcafin.Application.Dto
{
    public class PaymentTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PaymentStatus Status { get; set; }
    }
}