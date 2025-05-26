using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponse(u));
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? new UserResponse(user) : null;
        }

        public async Task<UserResponse> CreateAsync(UserCreateRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password) // ou outra lógica de hash
            };

            await _userRepository.AddAsync(user);
            return new UserResponse(user);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateRequest request)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            existingUser.Name = request.Name;
            existingUser.Email = request.Email;

            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
