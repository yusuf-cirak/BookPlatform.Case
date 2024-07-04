using FluentValidation;

namespace BookPlatform.Application.Features.Books.Commands.Delete;

public sealed class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommandRequest>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(s => s.Id).NotEmpty().NotNull();
    }
}