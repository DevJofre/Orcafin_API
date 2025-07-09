using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface ISubscriptionPlanService
    {
        Task<SubscriptionPlanResponse> CreateSubscriptionPlanAsync(SubscriptionPlanRequest request);
        Task<SubscriptionPlanResponse> GetSubscriptionPlanByIdAsync(int id);
        Task<IEnumerable<SubscriptionPlanResponse>> GetAllSubscriptionPlansAsync();
        Task<SubscriptionPlanResponse> UpdateSubscriptionPlanAsync(int id, SubscriptionPlanRequest request);
        Task DeleteSubscriptionPlanAsync(int id);
    }
}