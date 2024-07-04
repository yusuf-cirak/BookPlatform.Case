using FluentValidation;

namespace BookPlatform.Application.Features.BookNotes.Queries.Get;

public sealed class GetBookNoteByIdQueryValidator : AbstractValidator<GetBookNoteByIdQueryRequest>
{
    public GetBookNoteByIdQueryValidator()
    {
        RuleFor(s => s.Id)
            .NotNull()
            .NotEmpty();
    }
}