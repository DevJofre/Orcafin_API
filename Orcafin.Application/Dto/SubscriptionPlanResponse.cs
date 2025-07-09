using Orcafin.Domain.Enums;

namespace Orcafin.Application.Dto
{
    public class SubscriptionPlanResponse
    {
        public int Id { get; set; }
        public SubscriptionType Type { get; set; }
        public string GatewayId { get; set; }
    }
}