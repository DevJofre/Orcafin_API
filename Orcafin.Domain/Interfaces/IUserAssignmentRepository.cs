using Orcafin.Domain.Entities;

namespace Orcafin.Domain.Interfaces
{
    public interface IUserAssignmentRepository
    {
        Task<UserAssignment> AddAsync(UserAssignment userAssignment);
        Task<UserAssignment> GetByIdAsync(int id);
        Task<IEnumerable<UserAssignment>> GetAllAsync();
        Task UpdateAsync(UserAssignment userAssignment);
        Task DeleteAsync(int id);
    }
}