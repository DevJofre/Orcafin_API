using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;

namespace Orcafin.Application.Services
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TransactionHistoryService(ITransactionHistoryRepository repository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<TransactionHistoryResponse> CreateTransactionHistoryAsync(TransactionHistoryRequest request)
        {
            // Validar se o usuário e a categoria existem
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null) throw new ArgumentException($"Categoria com ID {request.CategoryId} não encontrada.");

            var transactionHistory = new TransactionHistory
            {
                UserId = request.UserId,
                Identifier = request.Identifier,
                Description = request.Description,
                Amount = request.Amount,
                Type = request.Type,
                CategoryId = request.CategoryId,
                TransactionAt = request.TransactionAt
            };
            var createdTransactionHistory = await _repository.AddAsync(transactionHistory);
            return new TransactionHistoryResponse
            {
                Id = createdTransactionHistory.Id,
                UserId = createdTransactionHistory.UserId,
                Identifier = createdTransactionHistory.Identifier,
                Description = createdTransactionHistory.Description,
                Amount = createdTransactionHistory.Amount,
                Type = createdTransactionHistory.Type,
                CategoryId = createdTransactionHistory.CategoryId,
                TransactionAt = createdTransactionHistory.TransactionAt
            };
        }

        public async Task<TransactionHistoryResponse> GetTransactionHistoryByIdAsync(int id)
        {
            var transactionHistory = await _repository.GetByIdAsync(id);
            if (transactionHistory == null) return null;
            return new TransactionHistoryResponse
            {
                Id = transactionHistory.Id,
                UserId = transactionHistory.UserId,
                Identifier = transactionHistory.Identifier,
                Description = transactionHistory.Description,
                Amount = transactionHistory.Amount,
                Type = transactionHistory.Type,
                CategoryId = transactionHistory.CategoryId,
                TransactionAt = transactionHistory.TransactionAt
            };
        }

        public async Task<IEnumerable<TransactionHistoryResponse>> GetAllTransactionHistoryAsync()
        {
            var transactionHistories = await _repository.GetAllAsync();
            return transactionHistories.Select(th => new TransactionHistoryResponse
            {
                Id = th.Id,
                UserId = th.UserId,
                Identifier = th.Identifier,
                Description = th.Description,
                Amount = th.Amount,
                Type = th.Type,
                CategoryId = th.CategoryId,
                TransactionAt = th.TransactionAt
            });
        }

        public async Task<TransactionHistoryResponse> UpdateTransactionHistoryAsync(int id, TransactionHistoryRequest request)
        {
            var transactionHistoryToUpdate = await _repository.GetByIdAsync(id);
            if (transactionHistoryToUpdate == null) return null;

            // Validar se o usuário e a categoria existem, caso sejam alterados
            if (transactionHistoryToUpdate.UserId != request.UserId)
            {
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null) throw new ArgumentException($"Usuário com ID {request.UserId} não encontrado.");
            }

            if (transactionHistoryToUpdate.CategoryId != request.CategoryId)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
                if (category == null) throw new ArgumentException($"Categoria com ID {request.CategoryId} não encontrada.");
            }

            transactionHistoryToUpdate.UserId = request.UserId;
            transactionHistoryToUpdate.Identifier = request.Identifier;
            transactionHistoryToUpdate.Description = request.Description;
            transactionHistoryToUpdate.Amount = request.Amount;
            transactionHistoryToUpdate.Type = request.Type;
            transactionHistoryToUpdate.CategoryId = request.CategoryId;
            transactionHistoryToUpdate.TransactionAt = request.TransactionAt;

            await _repository.UpdateAsync(transactionHistoryToUpdate);
            return new TransactionHistoryResponse
            {
                Id = transactionHistoryToUpdate.Id,
                UserId = transactionHistoryToUpdate.UserId,
                Identifier = transactionHistoryToUpdate.Identifier,
                Description = transactionHistoryToUpdate.Description,
                Amount = transactionHistoryToUpdate.Amount,
                Type = transactionHistoryToUpdate.Type,
                CategoryId = transactionHistoryToUpdate.CategoryId,
                TransactionAt = transactionHistoryToUpdate.TransactionAt
            };
        }

        public async Task<bool> DeleteTransactionHistoryAsync(int id)
        {
            var transactionHistory = await _repository.GetByIdAsync(id);
            if (transactionHistory == null)
            {
                return false;
            }
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}