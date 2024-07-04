using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed record UpdateBookNoteDto
{
    public string BookNoteId { get; init; }
    public string Note { get; init; }
    public ShareType ShareType { get; init; }
}