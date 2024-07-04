using BookPlatform.Application.Features.UserFriends.Commands.Create;
using FluentValidation;

namespace BookPlatform.Application.Features.UserFriends.Commands;

public sealed class CreateUserFriendCommandValidator : AbstractValidator<CreateUserFriendCommandRequest>
{
    public CreateUserFriendCommandValidator()
    {
        RuleFor(s => s.UserId)
            .NotEmpty()
            .NotNull();
    }
}