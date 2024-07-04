using FluentValidation;

namespace BookPlatform.Application.Features.Users.Commands.UpdateUsername;

public sealed  class UpdateUsernameCommandValidator : AbstractValidator<UpdateUsernameCommandRequest>
{
    public UpdateUsernameCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MaximumLength(50)
            .WithMessage("Username must not exceed 50 characters");
    }
}