using Orcafin.Domain.Entities;

namespace Orcafin.Domain.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<PaymentType> AddAsync(PaymentType paymentType);
        Task<PaymentType> GetByIdAsync(int id);
        Task<IEnumerable<PaymentType>> GetAllAsync();
        Task UpdateAsync(PaymentType paymentType);
        Task DeleteAsync(int id);
    }
}