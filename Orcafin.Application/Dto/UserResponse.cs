using Orcafin.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orcafin.Domain.Entities;

namespace Orcafin.Application.Dto
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Cpf { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public decimal Balance { get; set; }
        public UserStatus Status { get; set; }

        public UserResponse() { }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Login = user.Login;
            Cpf = user.Cpf;
            PhoneNumber = user.PhoneNumber;
            Role = user.Role;
            Balance = user.Balance;
            Status = user.Status;
        }

        public User ToEntity()
        {
            return new User
            {
                Id = Id,
                Name = Name,
                Email = Email,
                PasswordHash = "" // definir se necessário
            };
        }
    }
}

