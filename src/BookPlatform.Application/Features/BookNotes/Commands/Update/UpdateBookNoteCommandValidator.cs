using FluentValidation;

namespace BookPlatform.Application.Features.BookNotes.Commands.Update;

public sealed class UpdateBookNoteCommandValidator : AbstractValidator<UpdateBookNoteCommandRequest>
{
    public UpdateBookNoteCommandValidator()
    {
        RuleFor(s => s.BookNoteId).NotEmpty();
        RuleFor(s => s.Note).NotEmpty();
        RuleFor(s => s.ShareType).IsInEnum();
    }
}