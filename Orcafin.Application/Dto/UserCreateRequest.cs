using Orcafin.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Orcafin.Application.Dto
{
    public class UserCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public UserRole? Role { get; set; }
        public decimal? Balance { get; set; }
        public UserStatus? Status { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
