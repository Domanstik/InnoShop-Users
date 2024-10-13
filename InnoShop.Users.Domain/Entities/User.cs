using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.Users.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User(string name, string email, string role, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Role = role;
        PasswordHash = passwordHash;
        IsEmailConfirmed = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }

    public void UpdateUserInfo(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
    }

}
