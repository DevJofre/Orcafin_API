using Orcafin.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orcafin.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; } = UserRole.USER;
        public decimal Balance { get; set; } = 0.00m;
        public UserStatus Status { get; set; } = UserStatus.ENABLED;
        public string PasswordHash { get; set; }
    }

}
