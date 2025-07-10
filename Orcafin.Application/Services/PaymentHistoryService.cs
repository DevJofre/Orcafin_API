using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IPaymentHistoryRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;

        public PaymentHistoryService(IPaymentHistoryRepository repository, IUserRepository userRepository, IPaymentTypeRepository paymentTypeRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _paymentTypeRepository = paymentTypeRepository;
        }

        public async Task<PaymentHistoryResponse> CreatePaymentHistoryAsync(PaymentHistoryRequest request)
        {
            // Validar se o usuário e o tipo de pagamento existem
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");

            var paymentType = await _paymentTypeRepository.GetByIdAsync(request.PaymentTypeId);
            if (paymentType == null) throw new ArgumentException($"Tipo de pagamento com ID {request.PaymentTypeId} não encontrado.");

            var paymentHistory = new PaymentHistory
            {
                UserId = request.UserId,
                PaidAt = request.PaidAt,
                Status = request.Status,
                PaymentTypeId = request.PaymentTypeId
            };
            var createdPaymentHistory = await _repository.AddAsync(paymentHistory);
            return new PaymentHistoryResponse
            {
                Id = createdPaymentHistory.Id,
                UserId = createdPaymentHistory.UserId,
                PaidAt = createdPaymentHistory.PaidAt,
                Status = createdPaymentHistory.Status,
                PaymentTypeId = createdPaymentHistory.PaymentTypeId
            };
        }

        public async Task<PaymentHistoryResponse> GetPaymentHistoryByIdAsync(int id)
        {
            var paymentHistory = await _repository.GetByIdAsync(id);
            if (paymentHistory == null) return null;
            return new PaymentHistoryResponse
            {
                Id = paymentHistory.Id,
                UserId = paymentHistory.UserId,
                PaidAt = paymentHistory.PaidAt,
                Status = paymentHistory.Status,
                PaymentTypeId = paymentHistory.PaymentTypeId
            };
        }

        public async Task<IEnumerable<PaymentHistoryResponse>> GetAllPaymentHistoryAsync()
        {
            var paymentHistories = await _repository.GetAllAsync();
            return paymentHistories.Select(ph => new PaymentHistoryResponse
            {
                Id = ph.Id,
                UserId = ph.UserId,
                PaidAt = ph.PaidAt,
                Status = ph.Status,
                PaymentTypeId = ph.PaymentTypeId
            });
        }

        public async Task<PaymentHistoryResponse> UpdatePaymentHistoryAsync(int id, PaymentHistoryRequest request)
        {
            var paymentHistoryToUpdate = await _repository.GetByIdAsync(id);
            if (paymentHistoryToUpdate == null) return null;

            // Validar se o usuário e o tipo de pagamento existem, caso sejam alterados
            if (paymentHistoryToUpdate.UserId != request.UserId)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");
            }

            if (paymentHistoryToUpdate.PaymentTypeId != request.PaymentTypeId)
            {
                var paymentType = await _paymentTypeRepository.GetByIdAsync(request.PaymentTypeId);
                if (paymentType == null) throw new ArgumentException($"Tipo de pagamento com ID {request.PaymentTypeId} não encontrado.");
            }

            paymentHistoryToUpdate.UserId = request.UserId;
            paymentHistoryToUpdate.PaidAt = request.PaidAt;
            paymentHistoryToUpdate.Status = request.Status;
            paymentHistoryToUpdate.PaymentTypeId = request.PaymentTypeId;

            await _repository.UpdateAsync(paymentHistoryToUpdate);
            return new PaymentHistoryResponse
            {
                Id = paymentHistoryToUpdate.Id,
                UserId = paymentHistoryToUpdate.UserId,
                PaidAt = paymentHistoryToUpdate.PaidAt,
                Status = paymentHistoryToUpdate.Status,
                PaymentTypeId = paymentHistoryToUpdate.PaymentTypeId
            };
        }

        public async Task<bool> DeletePaymentHistoryAsync(int id)
        {
            var paymentHistory = await _repository.GetByIdAsync(id);
            if (paymentHistory == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}