using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed record GetBookNoteDto(string BookId, string BookNoteId, string UserId, string Note, ShareType ShareType);