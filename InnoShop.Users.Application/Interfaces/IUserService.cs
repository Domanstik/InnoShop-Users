using InnoShop.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace InnoShop.Users.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(string name, string email, string password);
    Task<string> LoginAsync(string email, string password);
    Task UpdateUserAsync(Guid userId, string name, string email, string role);
    Task DeleteUserAsync(Guid userId);
    Task<User> GetUserByIdAsync(Guid userId);
    Task ResetPasswordAsync(string email, string newPassword);
}
