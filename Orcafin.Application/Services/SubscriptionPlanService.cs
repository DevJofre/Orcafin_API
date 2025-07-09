using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _repository;

        public SubscriptionPlanService(ISubscriptionPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubscriptionPlanResponse> CreateSubscriptionPlanAsync(SubscriptionPlanRequest request)
        {
            var plan = new SubscriptionPlan
            {
                Type = request.Type,
                GatewayId = request.GatewayId
            };
            var createdPlan = await _repository.AddAsync(plan);
            return new SubscriptionPlanResponse
            {
                Id = createdPlan.Id,
                Type = createdPlan.Type,
                GatewayId = createdPlan.GatewayId
            };
        }

        public async Task<SubscriptionPlanResponse> GetSubscriptionPlanByIdAsync(int id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return null;
            return new SubscriptionPlanResponse
            {
                Id = plan.Id,
                Type = plan.Type,
                GatewayId = plan.GatewayId
            };
        }

        public async Task<IEnumerable<SubscriptionPlanResponse>> GetAllSubscriptionPlansAsync()
        {
            var plans = await _repository.GetAllAsync();
            return plans.Select(plan => new SubscriptionPlanResponse
            {
                Id = plan.Id,
                Type = plan.Type,
                GatewayId = plan.GatewayId
            });
        }

        public async Task<SubscriptionPlanResponse> UpdateSubscriptionPlanAsync(int id, SubscriptionPlanRequest request)
        {
            var planToUpdate = await _repository.GetByIdAsync(id);
            if (planToUpdate == null) return null;

            planToUpdate.Type = request.Type;
            planToUpdate.GatewayId = request.GatewayId;

            await _repository.UpdateAsync(planToUpdate);
            return new SubscriptionPlanResponse
            {
                Id = planToUpdate.Id,
                Type = planToUpdate.Type,
                GatewayId = planToUpdate.GatewayId
            };
        }

        public async Task DeleteSubscriptionPlanAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}