using FluentValidation;

namespace BookPlatform.Application.Features.UserFriends.Commands.Delete;

public sealed class DeleteUserFriendCommandValidator : AbstractValidator<DeleteUserFriendCommandRequest>
{
    public DeleteUserFriendCommandValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty()
            .NotNull();
    }
}