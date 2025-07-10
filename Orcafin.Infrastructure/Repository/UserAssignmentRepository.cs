using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class UserAssignmentRepository : IUserAssignmentRepository
    {
        private readonly OrcafinDbContext _context;

        public UserAssignmentRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task<UserAssignment> AddAsync(UserAssignment userAssignment)
        {
            _context.UserAssignments.Add(userAssignment);
            await _context.SaveChangesAsync();
            return userAssignment;
        }

        public async Task<UserAssignment> GetByIdAsync(int id)
        {
            return await _context.UserAssignments
                                 .Include(ua => ua.User)
                                 .Include(ua => ua.SubscriptionPlan)
                                 .FirstOrDefaultAsync(ua => ua.Id == id);
        }

        public async Task<IEnumerable<UserAssignment>> GetAllAsync()
        {
            return await _context.UserAssignments
                                 .Include(ua => ua.User)
                                 .Include(ua => ua.SubscriptionPlan)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(UserAssignment userAssignment)
        {
            _context.Entry(userAssignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userAssignment = await _context.UserAssignments.FindAsync(id);
            if (userAssignment != null)
            {
                _context.UserAssignments.Remove(userAssignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}