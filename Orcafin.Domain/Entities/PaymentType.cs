using Orcafin.Domain.Enums;

namespace Orcafin.Domain.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PaymentStatus Status { get; set; }
    }
}