using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed record CreateBookNoteDto(string BookId, string Note, ShareType ShareType);