using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orcafin.Domain.Interfaces;
using Orcafin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Orcafin.Infrastructure.Context;

namespace Orcafin.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OrcafinDbContext _context;

        public UserRepository(OrcafinDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> GetByLoginAsync(string login) => await _context.Users.FirstOrDefaultAsync(u => u.Login == login);

        public async Task<User> GetByCpfAsync(string cpf) => await _context.Users.FirstOrDefaultAsync(u => u.Cpf == cpf);

        public async Task<User> GetByPhoneNumberAsync(string phoneNumber) => await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

        public async Task UpdateAsync(User user)
        { 
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
