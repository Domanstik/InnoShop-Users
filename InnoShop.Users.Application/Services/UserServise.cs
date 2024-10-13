using InnoShop.Users.Application.Interfaces;
using InnoShop.Users.Domain.Entities;
using FluentValidation;
using System;
using System.Threading.Tasks;
using InnoShop.Users.Domain.Repositories;

namespace InnoShop.Users.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenService tokenService;
        private readonly IValidator<User> validator;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher,
                           ITokenService tokenService, IValidator<User> validator)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.tokenService = tokenService;
            this.validator = validator;
        }

        public async Task<User> CreateUserAsync(string name, string email, string password)
        {
            var user = new User(name, email, "User", passwordHasher.HashPassword(password));
            await ValidateUserAsync(user);
            await CheckUserEmailUniquenessAsync(email);
            await userRepository.CreateAsync(user);
            return user;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (!passwordHasher.VerifyPassword(user.PasswordHash, password))
            {
                throw new Exception("Invalid email or password.");
            }
            return tokenService.GenerateToken(user.Id, user.Email);
        }

        public async Task UpdateUserAsync(Guid userId, string name, string email, string role)
        {
            var user = await GetUserByIdAsync(userId);
            user.UpdateUserInfo(name, email, role);
            await ValidateUserAsync(user);
            await CheckUserEmailUniquenessAsync(email);
            await userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await userRepository.DeleteAsync(userId);
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            await EnsureUserExistsAsync(user, "User not found.");
            return user;
        }

        public async Task ResetPasswordAsync(string email, string newPassword)
        {
            var user = await GetUserByEmailAsync(email);
            user.UpdatePassword(passwordHasher.HashPassword(newPassword));
            await userRepository.UpdateAsync(user);
        }

        private async Task ValidateUserAsync(User user)
        {
            var validationResult = await validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        private async Task CheckUserEmailUniquenessAsync(string email)
        {
            var existingUser = await userRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }
        }

        private async Task EnsureUserExistsAsync(User user, string errorMessage)
        {
            if (user == null)
            {
                throw new Exception(errorMessage);
            }
        }

        private async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await userRepository.GetByEmailAsync(email);
            await EnsureUserExistsAsync(user, "User not found.");
            return user;
        }
    }
}
