using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface IPaymentHistoryService
    {
        Task<PaymentHistoryResponse> CreatePaymentHistoryAsync(PaymentHistoryRequest request);
        Task<PaymentHistoryResponse> GetPaymentHistoryByIdAsync(int id);
        Task<IEnumerable<PaymentHistoryResponse>> GetAllPaymentHistoryAsync();
        Task<PaymentHistoryResponse> UpdatePaymentHistoryAsync(int id, PaymentHistoryRequest request);
        Task<bool> DeletePaymentHistoryAsync(int id);
    }
}