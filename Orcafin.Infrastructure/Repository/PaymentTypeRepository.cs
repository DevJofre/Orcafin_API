using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly OrcafinDbContext _context;

        public PaymentTypeRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentType> AddAsync(PaymentType paymentType)
        {
            _context.PaymentTypes.Add(paymentType);
            await _context.SaveChangesAsync();
            return paymentType;
        }

        public async Task<PaymentType> GetByIdAsync(int id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentType>> GetAllAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task UpdateAsync(PaymentType paymentType)
        {
            _context.Entry(paymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(id);
            if (paymentType != null)
            {
                _context.PaymentTypes.Remove(paymentType);
                await _context.SaveChangesAsync();
            }
        }
    }
}