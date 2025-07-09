using Orcafin.Domain.Enums;

namespace Orcafin.Domain.Entities
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public SubscriptionType Type { get; set; }
        public string GatewayId { get; set; }
    }
}