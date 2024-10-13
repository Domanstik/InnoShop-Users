namespace InnoShop.Users.API.Models;

public class ResetPasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}
