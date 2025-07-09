using Moq;
using Orcafin.Application.Dto;
using Orcafin.Application.Services;
using Orcafin.Domain.Entities;
using Orcafin.Domain.Interfaces;
using Orcafin.Domain.Enums;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Orcafin.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "12345678901", // CPF válido para o teste
                PhoneNumber = "11987654321", // Telefone válido para o teste
                Password = "password"
            };
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email)).ReturnsAsync(new User { Email = request.Email });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("Email já existe.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenLoginAlreadyExists()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "12345678901", // CPF válido para o teste
                PhoneNumber = "11987654321", // Telefone válido para o teste
                Password = "password"
            };
            _userRepositoryMock.Setup(repo => repo.GetByLoginAsync(request.Login)).ReturnsAsync(new User { Login = request.Login });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("Login já existe.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenCpfAlreadyExists()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "12345678901",
                PhoneNumber = "11987654321", // Telefone válido para o teste
                Password = "password"
            };
            _userRepositoryMock.Setup(repo => repo.GetByCpfAsync(request.Cpf)).ReturnsAsync(new User { Cpf = request.Cpf });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("CPF já existe.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenCpfIsInvalid()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "123", // CPF inválido
                PhoneNumber = "11987654321", // Telefone válido para o teste
                Password = "password"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("CPF inválido. Deve conter exatamente 11 números.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenPhoneNumberAlreadyExists()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "12345678901",
                PhoneNumber = "11987654321",
                Password = "password"
            };
            _userRepositoryMock.Setup(repo => repo.GetByPhoneNumberAsync(request.PhoneNumber)).ReturnsAsync(new User { PhoneNumber = request.PhoneNumber });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("Número de telefone já existe.", exception.Message);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var request = new UserCreateRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Login = "test.user",
                Cpf = "12345678901",
                PhoneNumber = "123", // Telefone inválido
                Password = "password"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.CreateAsync(request));
            Assert.Equal("Número de telefone inválido. Deve conter entre 10 e 11 números.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenEmailAlreadyExistsForAnotherUser()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "another@example.com",
                Login = "updated.user",
                Cpf = "22222222222",
                PhoneNumber = "11888888888" // Telefone válido para o teste
            };
            var userWithSameEmail = new User { Id = 2, Name = "Another User", Email = "another@example.com", Login = "another.user", Cpf = "33333333333", PhoneNumber = "11777777777" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email)).ReturnsAsync(userWithSameEmail);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("Email já existe.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenLoginAlreadyExistsForAnotherUser()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Login = "another.user",
                Cpf = "22222222222",
                PhoneNumber = "11888888888" // Telefone válido para o teste
            };
            var userWithSameLogin = new User { Id = 2, Name = "Another User", Email = "another@example.com", Login = "another.user", Cpf = "33333333333", PhoneNumber = "11777777777" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.GetByLoginAsync(request.Login)).ReturnsAsync(userWithSameLogin);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("Login já existe.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenCpfAlreadyExistsForAnotherUser()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Login = "updated.user",
                Cpf = "22222222222",
                PhoneNumber = "11888888888" // Telefone válido para o teste
            };
            var userWithSameCpf = new User { Id = 2, Name = "Another User", Email = "another@example.com", Login = "another.user", Cpf = "22222222222", PhoneNumber = "11777777777" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.GetByCpfAsync(request.Cpf)).ReturnsAsync(userWithSameCpf);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("CPF já existe.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenCpfIsInvalid()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Login = "updated.user",
                Cpf = "123", // CPF inválido
                PhoneNumber = "11888888888" // Telefone válido para o teste
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("CPF inválido. Deve conter exatamente 11 números.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenPhoneNumberAlreadyExistsForAnotherUser()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Login = "updated.user",
                Cpf = "22222222222",
                PhoneNumber = "11888888888"
            };
            var userWithSamePhoneNumber = new User { Id = 2, Name = "Another User", Email = "another@example.com", Login = "another.user", Cpf = "33333333333", PhoneNumber = "11888888888" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.GetByPhoneNumberAsync(request.PhoneNumber)).ReturnsAsync(userWithSamePhoneNumber);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("Número de telefone já existe.", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenPhoneNumberIsInvalid()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "Existing User", Email = "existing@example.com", Login = "existing.user", Cpf = "11111111111", PhoneNumber = "11999999999" };
            var request = new UserUpdateRequest
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Login = "updated.user",
                Cpf = "22222222222",
                PhoneNumber = "123" // Telefone inválido
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.UpdateAsync(existingUser.Id, request));
            Assert.Equal("Número de telefone inválido. Deve conter entre 10 e 11 números.", exception.Message);
        }
    }
}