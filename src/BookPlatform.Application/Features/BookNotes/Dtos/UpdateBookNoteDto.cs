using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Dtos;

public sealed record UpdateBookNoteDto(string BookNoteId, string Note, ShareType ShareType);