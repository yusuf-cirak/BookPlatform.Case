using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed record GetBookNoteDto
{
    public string BookId { get; set; }
    public string BookNoteId { get; set; }
    public string UserId { get; set; }
    public string Note { get; set; }
    public ShareType ShareType { get; set; }
}