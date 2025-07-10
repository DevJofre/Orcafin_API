using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface IUserAssignmentService
    {
        Task<UserAssignmentResponse> CreateUserAssignmentAsync(UserAssignmentRequest request);
        Task<UserAssignmentResponse> GetUserAssignmentByIdAsync(int id);
        Task<IEnumerable<UserAssignmentResponse>> GetAllUserAssignmentsAsync();
        Task<UserAssignmentResponse> UpdateUserAssignmentAsync(int id, UserAssignmentRequest request);
        Task<bool> DeleteUserAssignmentAsync(int id);
    }
}