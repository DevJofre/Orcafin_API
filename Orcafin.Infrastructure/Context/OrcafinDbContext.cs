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
        public DbSet<UserAssignment> UserAssignments { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica a configuração da entidade User (e outras que você criar futuramente)
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new UserAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionHistoryConfiguration());

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

            if (!SubscriptionPlans.Any())
            {
                var plans = new List<SubscriptionPlan>
                {
                    new SubscriptionPlan { Id = 1, Type = SubscriptionType.MENSAL, GatewayId = "plan_mensal_001" },
                    new SubscriptionPlan { Id = 2, Type = SubscriptionType.TRIMESTRAL, GatewayId = "plan_trimestral_002" },
                    new SubscriptionPlan { Id = 3, Type = SubscriptionType.ANUAL, GatewayId = "plan_anual_003" }
                };
                await SubscriptionPlans.AddRangeAsync(plans);
                await SaveChangesAsync();
            }

            if (!PaymentTypes.Any())
            {
                var paymentTypes = new List<PaymentType>
                {
                    new PaymentType { Id = 1, Name = "Cartão de Crédito", Status = PaymentStatus.ENABLED },
                    new PaymentType { Id = 2, Name = "PIX", Status = PaymentStatus.ENABLED },
                    new PaymentType { Id = 3, Name = "Boleto", Status = PaymentStatus.DISABLED }
                };
                await PaymentTypes.AddRangeAsync(paymentTypes);
                await SaveChangesAsync();
            }

            if (!Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Id = 1, Name = "Alimentação" },
                    new Category { Id = 2, Name = "Transporte" },
                    new Category { Id = 3, Name = "Salário" },
                    new Category { Id = 4, Name = "Lazer" }
                };
                await Categories.AddRangeAsync(categories);
                await SaveChangesAsync();
            }

            if (!UserAssignments.Any())
            {
                var userAssignments = new List<UserAssignment>
                {
                    new UserAssignment
                    {
                        Id = 1,
                        UserId = 1, // User 1
                        PlanId = 1, // Mensal
                        PaymentMethod = PaymentMethodType.PIX,
                        LastPaymentDate = DateTime.UtcNow.AddMonths(-1),
                        NextPaymentDate = DateTime.UtcNow.AddDays(15)
                    },
                    new UserAssignment
                    {
                        Id = 2,
                        UserId = 2, // User 2
                        PlanId = 3, // Anual
                        PaymentMethod = PaymentMethodType.CARTAO,
                        LastPaymentDate = DateTime.UtcNow.AddMonths(-3),
                        NextPaymentDate = DateTime.UtcNow.AddMonths(9)
                    }
                };
                await UserAssignments.AddRangeAsync(userAssignments);
                await SaveChangesAsync();
            }

            if (!PaymentHistory.Any())
            {
                var paymentHistories = new List<PaymentHistory>
                {
                    new PaymentHistory
                    {
                        Id = 1,
                        UserId = 1, // User 1
                        PaidAt = DateTime.UtcNow.AddMonths(-1),
                        Status = PaymentStatusEnum.COMPLETED,
                        PaymentTypeId = 2 // PIX
                    },
                    new PaymentHistory
                    {
                        Id = 2,
                        UserId = 2, // User 2
                        PaidAt = DateTime.UtcNow.AddMonths(-3),
                        Status = PaymentStatusEnum.FAILED,
                        PaymentTypeId = 1 // Cartão de Crédito
                    }
                };
                await PaymentHistory.AddRangeAsync(paymentHistories);
                await SaveChangesAsync();
            }

            if (!TransactionHistory.Any())
            {
                var transactionHistories = new List<TransactionHistory>
                {
                    new TransactionHistory
                    {
                        Id = 1,
                        UserId = 1, // User 1
                        Identifier = "TRANS001",
                        Description = "Almoço no restaurante",
                        Amount = 45.50m,
                        Type = TransactionType.DESPESA,
                        CategoryId = 1, // Alimentação
                        TransactionAt = DateTime.UtcNow.AddDays(-5)
                    },
                    new TransactionHistory
                    {
                        Id = 2,
                        UserId = 1, // User 1
                        Identifier = "TRANS002",
                        Description = "Salário mensal",
                        Amount = 3000.00m,
                        Type = TransactionType.RECEITA,
                        CategoryId = 3, // Salário
                        TransactionAt = DateTime.UtcNow.AddDays(-2)
                    },
                    new TransactionHistory
                    {
                        Id = 3,
                        UserId = 2, // User 2
                        Identifier = "TRANS003",
                        Description = "Passagem de ônibus",
                        Amount = 5.00m,
                        Type = TransactionType.DESPESA,
                        CategoryId = 2, // Transporte
                        TransactionAt = DateTime.UtcNow.AddDays(-1)
                    }
                };
                await TransactionHistory.AddRangeAsync(transactionHistories);
                await SaveChangesAsync();
            }
        }
    }
}