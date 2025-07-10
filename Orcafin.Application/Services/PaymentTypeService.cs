using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _repository;

        public PaymentTypeService(IPaymentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaymentTypeResponse> CreatePaymentTypeAsync(PaymentTypeRequest request)
        {
            var paymentType = new PaymentType
            {
                Name = request.Name,
                Status = request.Status
            };
            var createdPaymentType = await _repository.AddAsync(paymentType);
            return new PaymentTypeResponse
            {
                Id = createdPaymentType.Id,
                Name = createdPaymentType.Name,
                Status = createdPaymentType.Status
            };
        }

        public async Task<PaymentTypeResponse> GetPaymentTypeByIdAsync(int id)
        {
            var paymentType = await _repository.GetByIdAsync(id);
            if (paymentType == null) return null;
            return new PaymentTypeResponse
            {
                Id = paymentType.Id,
                Name = paymentType.Name,
                Status = paymentType.Status
            };
        }

        public async Task<IEnumerable<PaymentTypeResponse>> GetAllPaymentTypesAsync()
        {
            var paymentTypes = await _repository.GetAllAsync();
            return paymentTypes.Select(paymentType => new PaymentTypeResponse
            {
                Id = paymentType.Id,
                Name = paymentType.Name,
                Status = paymentType.Status
            });
        }

        public async Task<PaymentTypeResponse> UpdatePaymentTypeAsync(int id, PaymentTypeRequest request)
        {
            var paymentTypeToUpdate = await _repository.GetByIdAsync(id);
            if (paymentTypeToUpdate == null) return null;

            paymentTypeToUpdate.Name = request.Name;
            paymentTypeToUpdate.Status = request.Status;

            await _repository.UpdateAsync(paymentTypeToUpdate);
            return new PaymentTypeResponse
            {
                Id = paymentTypeToUpdate.Id,
                Name = paymentTypeToUpdate.Name,
                Status = paymentTypeToUpdate.Status
            };
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}