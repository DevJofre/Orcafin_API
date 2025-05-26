using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<UserResponse?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(UserCreateRequest request);
        Task<bool> UpdateAsync(int id, UserUpdateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
