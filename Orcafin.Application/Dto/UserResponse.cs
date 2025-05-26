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

        public UserResponse() { }

        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
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

