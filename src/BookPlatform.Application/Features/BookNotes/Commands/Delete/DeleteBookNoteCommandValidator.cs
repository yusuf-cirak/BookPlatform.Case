using FluentValidation;

namespace BookPlatform.Application.Features.BookNotes.Commands.Delete;

public sealed class DeleteBookNoteCommandValidator : AbstractValidator<DeleteBookNoteCommandRequest>
{
    public DeleteBookNoteCommandValidator()
    {
        RuleFor(s => s.BookNoteId).NotEmpty()
            .NotNull();
    }
}