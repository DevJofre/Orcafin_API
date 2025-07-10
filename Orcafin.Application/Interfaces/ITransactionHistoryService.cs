using Orcafin.Application.Dto;

namespace Orcafin.Application.Interfaces
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistoryResponse> CreateTransactionHistoryAsync(TransactionHistoryRequest request);
        Task<TransactionHistoryResponse> GetTransactionHistoryByIdAsync(int id);
        Task<IEnumerable<TransactionHistoryResponse>> GetAllTransactionHistoryAsync();
        Task<TransactionHistoryResponse> UpdateTransactionHistoryAsync(int id, TransactionHistoryRequest request);
        Task<bool> DeleteTransactionHistoryAsync(int id);
    }
}