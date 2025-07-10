using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class UserAssignmentService : IUserAssignmentService
    {
        private readonly IUserAssignmentRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;

        public UserAssignmentService(IUserAssignmentRepository repository, IUserRepository userRepository, ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        public async Task<UserAssignmentResponse> CreateUserAssignmentAsync(UserAssignmentRequest request)
        {
            // Validar se o usuário e o plano existem
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");

            var plan = await _subscriptionPlanRepository.GetByIdAsync(request.PlanId);
            if (plan == null) throw new ArgumentException($"Plano de assinatura com ID {request.PlanId} não encontrado.");

            var userAssignment = new UserAssignment
            {
                UserId = request.UserId,
                PlanId = request.PlanId,
                PaymentMethod = request.PaymentMethod,
                LastPaymentDate = request.LastPaymentDate,
                NextPaymentDate = request.NextPaymentDate
            };
            var createdUserAssignment = await _repository.AddAsync(userAssignment);
            return new UserAssignmentResponse
            {
                Id = createdUserAssignment.Id,
                UserId = createdUserAssignment.UserId,
                PlanId = createdUserAssignment.PlanId,
                PaymentMethod = createdUserAssignment.PaymentMethod,
                LastPaymentDate = createdUserAssignment.LastPaymentDate,
                NextPaymentDate = createdUserAssignment.NextPaymentDate
            };
        }

        public async Task<UserAssignmentResponse> GetUserAssignmentByIdAsync(int id)
        {
            var userAssignment = await _repository.GetByIdAsync(id);
            if (userAssignment == null) return null;
            return new UserAssignmentResponse
            {
                Id = userAssignment.Id,
                UserId = userAssignment.UserId,
                PlanId = userAssignment.PlanId,
                PaymentMethod = userAssignment.PaymentMethod,
                LastPaymentDate = userAssignment.LastPaymentDate,
                NextPaymentDate = userAssignment.NextPaymentDate
            };
        }

        public async Task<IEnumerable<UserAssignmentResponse>> GetAllUserAssignmentsAsync()
        {
            var userAssignments = await _repository.GetAllAsync();
            return userAssignments.Select(ua => new UserAssignmentResponse
            {
                Id = ua.Id,
                UserId = ua.UserId,
                PlanId = ua.PlanId,
                PaymentMethod = ua.PaymentMethod,
                LastPaymentDate = ua.LastPaymentDate,
                NextPaymentDate = ua.NextPaymentDate
            });
        }

        public async Task<UserAssignmentResponse> UpdateUserAssignmentAsync(int id, UserAssignmentRequest request)
        {
            var userAssignmentToUpdate = await _repository.GetByIdAsync(id);
            if (userAssignmentToUpdate == null) return null;

            // Validar se o usuário e o plano existem, caso sejam alterados
            if (userAssignmentToUpdate.UserId != request.UserId)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");
            }

            if (userAssignmentToUpdate.PlanId != request.PlanId)
            {
                var plan = await _subscriptionPlanRepository.GetByIdAsync(request.PlanId);
                if (plan == null) throw new ArgumentException($"Plano de assinatura com ID {request.PlanId} não encontrado.");
            }

            userAssignmentToUpdate.UserId = request.UserId;
            userAssignmentToUpdate.PlanId = request.PlanId;
            userAssignmentToUpdate.PaymentMethod = request.PaymentMethod;
            userAssignmentToUpdate.LastPaymentDate = request.LastPaymentDate;
            userAssignmentToUpdate.NextPaymentDate = request.NextPaymentDate;

            await _repository.UpdateAsync(userAssignmentToUpdate);
            return new UserAssignmentResponse
            {
                Id = userAssignmentToUpdate.Id,
                UserId = userAssignmentToUpdate.UserId,
                PlanId = userAssignmentToUpdate.PlanId,
                PaymentMethod = userAssignmentToUpdate.PaymentMethod,
                LastPaymentDate = userAssignmentToUpdate.LastPaymentDate,
                NextPaymentDate = userAssignmentToUpdate.NextPaymentDate
            };
        }

        public async Task<bool> DeleteUserAssignmentAsync(int id)
        {
            var userAssignment = await _repository.GetByIdAsync(id);
            if (userAssignment == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}