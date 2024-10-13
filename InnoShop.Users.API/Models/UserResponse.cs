using System;

namespace InnoShop.Users.API.Models;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserResponse(Guid id, string name, string email, string role, bool isEmailConfirmed, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        IsEmailConfirmed = isEmailConfirmed;
        CreatedAt = createdAt;
    }
}
