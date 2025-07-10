using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<PaymentTypeResponse> CreatePaymentTypeAsync(PaymentTypeRequest request);
        Task<PaymentTypeResponse> GetPaymentTypeByIdAsync(int id);
        Task<IEnumerable<PaymentTypeResponse>> GetAllPaymentTypesAsync();
        Task<PaymentTypeResponse> UpdatePaymentTypeAsync(int id, PaymentTypeRequest request);
        Task DeletePaymentTypeAsync(int id);
    }
}