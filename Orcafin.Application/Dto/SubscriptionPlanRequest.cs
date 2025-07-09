using Orcafin.Domain.Enums;

namespace Orcafin.Application.Dto
{
    public class SubscriptionPlanRequest
    {
        public SubscriptionType Type { get; set; }
        public string GatewayId { get; set; }
    }
}