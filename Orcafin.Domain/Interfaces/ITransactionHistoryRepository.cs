using Orcafin.Domain.Entities;

namespace Orcafin.Domain.Interfaces
{
    public interface ITransactionHistoryRepository
    {
        Task<TransactionHistory> AddAsync(TransactionHistory transactionHistory);
        Task<TransactionHistory> GetByIdAsync(int id);
        Task<IEnumerable<TransactionHistory>> GetAllAsync();
        Task UpdateAsync(TransactionHistory transactionHistory);
        Task DeleteAsync(int id);
    }
}