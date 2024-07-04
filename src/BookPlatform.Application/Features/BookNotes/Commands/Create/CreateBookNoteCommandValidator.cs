using FluentValidation;

namespace BookPlatform.Application.Features.BookNotes.Commands.Create;

public sealed class CreateBookNoteCommandValidator : AbstractValidator<CreateBookNoteCommandRequest>
{
    public CreateBookNoteCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(x => x.Note)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(x => x.ShareType)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("{PropertyName} must be a valid ShareType.");
    }
}