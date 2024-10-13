using FluentValidation;
using InnoShop.Users.Domain.Entities;

namespace InnoShop.Users.Application.Validators;

public class UserValidator: AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(user => user.Role)
            .NotEmpty();
            

        RuleFor(user => user.PasswordHash)
            .NotEmpty()
            .MinimumLength(8);
    }
}
