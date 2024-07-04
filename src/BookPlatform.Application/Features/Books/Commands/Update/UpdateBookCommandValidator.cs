using FluentValidation;

namespace BookPlatform.Application.Features.Books.Commands.Update;

public sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommandRequest>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(s=>s.Id).NotEmpty().NotNull();
        RuleFor(s => s.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(s => s.Author)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(s => s.Isbn).NotEmpty().NotNull();
        
        RuleFor(s => s.Pages).NotEmpty().NotNull()
            .LessThan(100_000);
        
        RuleFor(s => s.Genre).NotEmpty().NotNull();
        
        RuleFor(s => s.PublishedDate).NotEmpty().NotNull();
        
        RuleFor(s => s.Description).NotEmpty().NotNull();
        
        RuleFor(s => s.ShelfLocation).NotNull();
    }
}