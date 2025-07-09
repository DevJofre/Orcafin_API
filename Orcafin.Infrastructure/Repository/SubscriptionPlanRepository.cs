using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly OrcafinDbContext _context;

        public SubscriptionPlanRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan)
        {
            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<SubscriptionPlan> GetByIdAsync(int id)
        {
            return await _context.SubscriptionPlans.FindAsync(id);
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync()
        {
            return await _context.SubscriptionPlans.ToListAsync();
        }

        public async Task UpdateAsync(SubscriptionPlan plan)
        {
            _context.Entry(plan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(id);
            if (plan != null)
            {
                _context.SubscriptionPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
    }
}