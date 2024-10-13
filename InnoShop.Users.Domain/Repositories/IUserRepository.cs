using InnoShop.Users.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace InnoShop.Users.Domain.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}
