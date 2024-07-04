using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Dtos;
using BookPlatform.Domain;
using BookPlatform.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.BookNotes.Services;

public interface IBookNoteService
{
    Task<BookNote?> GetBookNoteByIdAsync(string bookNoteId, CancellationToken cancellationToken = default);
    Task<List<GetBookNoteDto>> GetBookNotesAsync(string bookId, CancellationToken cancellationToken = default);
    BookNote CreateBookNote(string userId, string bookId, string note, ShareType shareType);

    Task<int> UpdateBookNoteAsync(UpdateBookNoteDto updateBookNoteDto, CancellationToken cancellationToken = default);

    Task<int> UpdateBookNoteShareTypeAsync(string bookNoteId, ShareType shareType,
        CancellationToken cancellationToken = default);

    Task<int> DeleteBookNoteAsync(string bookNoteId, CancellationToken cancellationToken = default);
}

public sealed class BookNoteService : BaseService, IBookNoteService
{
    public Task<BookNote?> GetBookNoteByIdAsync(string bookNoteId, CancellationToken cancellationToken = default)
    {
        return EfRepository
            .BookNotes
            .Where(bn => bn.Id == bookNoteId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public Task<List<GetBookNoteDto>> GetBookNotesAsync(string bookId,
        CancellationToken cancellationToken = default)
    {
        return EfRepository
            .BookNotes
            .Where(bn => bn.BookId == bookId)
            .Select(bn => Mapper.Map<GetBookNoteDto>(bn))
            .ToListAsync(cancellationToken);
    }

    public BookNote CreateBookNote(string userId, string bookId, string note, ShareType shareType)
    {
        var bookNote = new BookNote
        {
            UserId = userId,
            BookId = bookId,
            Note = note,
            ShareType = shareType
        };

        EfRepository.BookNotes.Add(bookNote);

        return bookNote;
    }

    public Task<int> UpdateBookNoteAsync(UpdateBookNoteDto updateBookNoteDto,
        CancellationToken cancellationToken = default)
    {
        return EfRepository
            .BookNotes
            .Where(bn => bn.Id == updateBookNoteDto.BookNoteId)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(bn => bn.Note, updateBookNoteDto.Note)
                    .SetProperty(bn => bn.ShareType, updateBookNoteDto.ShareType),
                cancellationToken);
    }

    public Task<int> UpdateBookNoteShareTypeAsync(string bookNoteId, ShareType shareType,
        CancellationToken cancellationToken = default)
    {
        return EfRepository
            .BookNotes
            .Where(bn => bn.Id == bookNoteId)
            .ExecuteUpdateAsync(spc => spc
                    .SetProperty(bn => bn.ShareType, shareType),
                cancellationToken);
    }

    public Task<int> DeleteBookNoteAsync(string bookNoteId, CancellationToken cancellationToken = default)
    {
        return EfRepository
            .BookNotes
            .Where(bn => bn.Id == bookNoteId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}