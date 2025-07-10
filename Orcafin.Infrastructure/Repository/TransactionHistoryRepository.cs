using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly OrcafinDbContext _context;

        public TransactionHistoryRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionHistory> AddAsync(TransactionHistory transactionHistory)
        {
            _context.TransactionHistory.Add(transactionHistory);
            await _context.SaveChangesAsync();
            return transactionHistory;
        }

        public async Task<TransactionHistory> GetByIdAsync(int id)
        {
            return await _context.TransactionHistory
                                 .Include(th => th.User)
                                 .Include(th => th.Category)
                                 .FirstOrDefaultAsync(th => th.Id == id);
        }

        public async Task<IEnumerable<TransactionHistory>> GetAllAsync()
        {
            return await _context.TransactionHistory
                                 .Include(th => th.User)
                                 .Include(th => th.Category)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(TransactionHistory transactionHistory)
        {
            _context.Entry(transactionHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            if (transactionHistory != null)
            {
                _context.TransactionHistory.Remove(transactionHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}