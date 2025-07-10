using Microsoft.EntityFrameworkCore;
using Orcafin.Domain.Entities;
using Orcafin.Infrastructure.Configuration;
using Orcafin.Domain.Enums;

namespace Orcafin.Infrastructure.Context
{
    public class OrcafinDbContext : DbContext
    {
        public OrcafinDbContext(DbContextOptions<OrcafinDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica a configuração da entidade User (e outras que você criar futuramente)
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task SeedData()
        {
            if (!Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Name = "Admin User",
                        Email = "admin@orafin.com",
                        Login = "admin.user",
                        Cpf = "11111111111",
                        PhoneNumber = "11911111111",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        Role = UserRole.ADMIN,
                        Balance = 1000.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Regular User 1",
                        Email = "user1@orafin.com",
                        Login = "user.one",
                        Cpf = "22222222222",
                        PhoneNumber = "11922222222",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 500.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Regular User 2",
                        Email = "user2@orafin.com",
                        Login = "user.two",
                        Cpf = "33333333333",
                        PhoneNumber = "11933333333",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 250.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Disabled User",
                        Email = "disabled@orafin.com",
                        Login = "disabled.user",
                        Cpf = "44444444444",
                        PhoneNumber = "11944444444",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 0.00m,
                        Status = UserStatus.DISABLED
                    },
                    new User
                    {
                        Name = "Manager User",
                        Email = "manager@orafin.com",
                        Login = "manager.user",
                        Cpf = "55555555555",
                        PhoneNumber = "11955555555",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"),
                        Role = UserRole.ADMIN,
                        Balance = 2000.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Test User 3",
                        Email = "user3@orafin.com",
                        Login = "user.three",
                        Cpf = "66666666666",
                        PhoneNumber = "11966666666",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 150.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Test User 4",
                        Email = "user4@orafin.com",
                        Login = "user.four",
                        Cpf = "77777777777",
                        PhoneNumber = "11977777777",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 300.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Test User 5",
                        Email = "user5@orafin.com",
                        Login = "user.five",
                        Cpf = "88888888888",
                        PhoneNumber = "11988888888",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 700.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Test User 6",
                        Email = "user6@orafin.com",
                        Login = "user.six",
                        Cpf = "99999999999",
                        PhoneNumber = "11999999999",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 900.00m,
                        Status = UserStatus.ENABLED
                    },
                    new User
                    {
                        Name = "Test User 7",
                        Email = "user7@orafin.com",
                        Login = "user.seven",
                        Cpf = "10101010101",
                        PhoneNumber = "11910101010",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                        Role = UserRole.USER,
                        Balance = 120.00m,
                        Status = UserStatus.ENABLED
                    }
                };

                await Users.AddRangeAsync(users);
                await SaveChangesAsync();
            }
        }
    }
}
