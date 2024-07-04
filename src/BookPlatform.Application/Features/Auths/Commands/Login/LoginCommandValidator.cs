using BookPlatform.Application.Features.Auths.Commands.Login;
using FluentValidation;

namespace Application.Features.Auths.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
{
    public LoginCommandValidator()
    {
        RuleFor(u => u.Username).NotEmpty().WithMessage("User name cannot be empty");
        RuleFor(u => u.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be longer than 6 characters");
    }
}