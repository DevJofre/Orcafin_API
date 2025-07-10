using Orcafin.Domain.Entities;

namespace Orcafin.Domain.Interfaces
{
    public interface IPaymentHistoryRepository
    {
        Task<PaymentHistory> AddAsync(PaymentHistory paymentHistory);
        Task<PaymentHistory> GetByIdAsync(int id);
        Task<IEnumerable<PaymentHistory>> GetAllAsync();
        Task UpdateAsync(PaymentHistory paymentHistory);
        Task DeleteAsync(int id);
    }
}