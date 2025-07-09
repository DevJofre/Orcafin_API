using Orcafin.Application.Dto;
using Orcafin.Application.Interfaces;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Orcafin.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponse(u));
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? new UserResponse(user) : null;
        }

        public async Task<UserResponse> CreateAsync(UserCreateRequest request)
        {
            if (!IsValidCpf(request.Cpf))
            {
                throw new Exception("CPF inválido. Deve conter exatamente 11 números.");
            }

            if (!IsValidPhoneNumber(request.PhoneNumber))
            {
                throw new Exception("Número de telefone inválido. Deve conter entre 10 e 11 números.");
            }

            var existingUserByEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUserByEmail != null)
            {
                throw new Exception("Email já existe.");
            }

            var existingUserByLogin = await _userRepository.GetByLoginAsync(request.Login);
            if (existingUserByLogin != null)
            {
                throw new Exception("Login já existe.");
            }

            var existingUserByCpf = await _userRepository.GetByCpfAsync(request.Cpf);
            if (existingUserByCpf != null)
            {
                throw new Exception("CPF já existe.");
            }

            var existingUserByPhoneNumber = await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (existingUserByPhoneNumber != null)
            {
                throw new Exception("Número de telefone já existe.");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Login = request.Login,
                Cpf = request.Cpf,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role ?? UserRole.USER, // Valor padrão
                Balance = request.Balance ?? 0.00m, // Valor padrão
                Status = request.Status ?? UserStatus.ENABLED // Valor padrão
            };

            await _userRepository.AddAsync(user);
            return new UserResponse(user);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateRequest request)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return false;

            if (!IsValidCpf(request.Cpf))
            {
                throw new Exception("CPF inválido. Deve conter exatamente 11 números.");
            }

            if (!IsValidPhoneNumber(request.PhoneNumber))
            {
                throw new Exception("Número de telefone inválido. Deve conter entre 10 e 11 números.");
            }

            // Verifica unicidade do email se ele foi alterado
            if (existingUser.Email != request.Email)
            {
                var userWithSameEmail = await _userRepository.GetByEmailAsync(request.Email);
                if (userWithSameEmail != null && userWithSameEmail.Id != id)
                {
                    throw new Exception("Email já existe.");
                }
            }

            // Verifica unicidade do login se ele foi alterado
            if (existingUser.Login != request.Login)
            {
                var userWithSameLogin = await _userRepository.GetByLoginAsync(request.Login);
                if (userWithSameLogin != null && userWithSameLogin.Id != id)
                {
                    throw new Exception("Login já existe.");
                }
            }

            // Verifica unicidade do CPF se ele foi alterado
            if (existingUser.Cpf != request.Cpf)
            {
                var userWithSameCpf = await _userRepository.GetByCpfAsync(request.Cpf);
                if (userWithSameCpf != null && userWithSameCpf.Id != id)
                {
                    throw new Exception("CPF já existe.");
                }
            }

            // Verifica unicidade do telefone se ele foi alterado
            if (existingUser.PhoneNumber != request.PhoneNumber)
            {
                var userWithSamePhoneNumber = await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber);
                if (userWithSamePhoneNumber != null && userWithSamePhoneNumber.Id != id)
                {
                    throw new Exception("Número de telefone já existe.");
                }
            }

            existingUser.Name = request.Name;
            existingUser.Email = request.Email;
            existingUser.Login = request.Login;
            existingUser.Cpf = request.Cpf;
            existingUser.PhoneNumber = request.PhoneNumber;
            existingUser.Role = request.Role ?? existingUser.Role;
            existingUser.Balance = request.Balance ?? existingUser.Balance;
            existingUser.Status = request.Status ?? existingUser.Status;

            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        private bool IsValidCpf(string cpf)
        {
            return cpf != null && cpf.Length == 11 && Regex.IsMatch(cpf, "^[0-9]{11}$");
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber != null && (phoneNumber.Length == 10 || phoneNumber.Length == 11) && Regex.IsMatch(phoneNumber, "^[0-9]+$");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
