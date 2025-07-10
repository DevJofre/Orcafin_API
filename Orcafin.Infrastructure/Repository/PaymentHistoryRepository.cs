using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        private readonly OrcafinDbContext _context;

        public PaymentHistoryRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentHistory> AddAsync(PaymentHistory paymentHistory)
        {
            _context.PaymentHistory.Add(paymentHistory);
            await _context.SaveChangesAsync();
            return paymentHistory;
        }

        public async Task<PaymentHistory> GetByIdAsync(int id)
        {
            return await _context.PaymentHistory
                                 .Include(ph => ph.User)
                                 .Include(ph => ph.PaymentType)
                                 .FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<IEnumerable<PaymentHistory>> GetAllAsync()
        {
            return await _context.PaymentHistory
                                 .Include(ph => ph.User)
                                 .Include(ph => ph.PaymentType)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(PaymentHistory paymentHistory)
        {
            _context.Entry(paymentHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paymentHistory = await _context.PaymentHistory.FindAsync(id);
            if (paymentHistory != null)
            {
                _context.PaymentHistory.Remove(paymentHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}