using InnoShop.Users.Domain.Entities;
using InnoShop.Users.Domain.Repositories;
using InnoShop.Users.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace InnoShop.Users.Infrastructure.Repositories;

public class UserRepository : IUserRepository 
{
    private readonly UserContext context;

    public UserRepository(UserContext context)
    {
        this.context = context;
    }

    //CreateUser
    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    //GetUserById
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    //GetUserByEmail
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    //UpdateUser
    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    //DeleteUser
    public async Task DeleteAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);

        if(user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
