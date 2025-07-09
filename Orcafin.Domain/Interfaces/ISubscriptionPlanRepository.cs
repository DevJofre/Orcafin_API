using Orcafin.Domain.Entities;

namespace Orcafin.Domain.Interfaces
{
    public interface ISubscriptionPlanRepository
    {
        Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan);
        Task<SubscriptionPlan> GetByIdAsync(int id);
        Task<IEnumerable<SubscriptionPlan>> GetAllAsync();
        Task UpdateAsync(SubscriptionPlan plan);
        Task DeleteAsync(int id);
    }
}