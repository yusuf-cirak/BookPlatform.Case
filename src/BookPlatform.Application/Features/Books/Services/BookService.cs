using System.Linq.Expressions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.Books.Dtos;
using BookPlatform.Domain;
using BookPlatform.Infrastructure.Image.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Application.Features.Books.Services;

public interface IBookService
{
    Task<Book?> GetBookByIdAsync(string bookNoteId, CancellationToken cancellationToken = default);

    Task<List<GetBookDto>> GetBooksByFilterAsync(string filter, CancellationToken cancellationToken = default);
    Task<Book> CreateBookAsync(CreateBookDto createBookDto);

    Task<string> CreateBookPictureAsync(string bookId, IFormFile? picture,
        CancellationToken cancellationToken = default);

    Task<Book> UpdateBookAsync(UpdateBookDto updateBookDto, CancellationToken cancellationToken = default);

    Task<int> DeleteBookAsync(string bookId, CancellationToken cancellationToken = default);
}

public sealed class BookService : BaseService, IBookService
{
    private readonly IImageService _imageService;

    public BookService(IImageService imageService)
    {
        _imageService = imageService;
    }

    public Task<Book?> GetBookByIdAsync(string bookNoteId, CancellationToken cancellationToken = default)
    {
        return EfRepository
            .Books
            .Where(b => b.Id == bookNoteId)
            .SingleOrDefaultAsync(cancellationToken);
    }


    public async Task<List<GetBookDto>> GetBooksByFilterAsync(string filter, CancellationToken cancellationToken)
    {
        // full-text search needs to be enabled in the database which requires an installation at DB level
        // var result = await EfRepository
        //     .Books
        //     .FromSqlInterpolated($@"
        //     SELECT *
        //     FROM Books
        //     WHERE CONTAINS((Title, Description, Author, Genre, Isbn), {filter})
        // ")
        //     .Select(b => Mapper.Map<GetBookDto>(b))
        //     .ToListAsync(cancellationToken);

        var result = await EfRepository
            .Books
            .Where(b => EF.Functions.Like(b.Title, $"%{filter}%") ||
                        EF.Functions.Like(b.Description, $"%{filter}%") ||
                        EF.Functions.Like(b.Author, $"%{filter}%") ||
                        EF.Functions.Like(b.Genre, $"%{filter}%") ||
                        EF.Functions.Like(b.Isbn, $"%{filter}%"))
            .Select(b => Mapper.Map<GetBookDto>(b))
            .ToListAsync(cancellationToken);

        return result;
    }


    public async Task<string> CreateBookPictureAsync(string bookId, IFormFile? picture,
        CancellationToken cancellationToken = default)
    {
        var pictureUrl = picture is not null
            ? await _imageService.UploadImageAsync(picture, "book-pictures", bookId,
                cancellationToken: cancellationToken)
            : string.Empty;

        return pictureUrl;
    }

    public async Task<bool> DeleteBookPictureAsync(string bookId,
        CancellationToken cancellationToken = default)
    {
        return await _imageService.RemoveImageAsync("book-pictures", bookId, cancellationToken: cancellationToken);
    }

    public async Task<Book> CreateBookAsync(CreateBookDto createBookDto)
    {
        var book = Mapper.Map<Book>(createBookDto);

        var pictureUrl = await CreateBookPictureAsync(book.Id, createBookDto.Picture);

        book.PictureUrl = pictureUrl;

        EfRepository.Books.Add(book);

        return book;
    }

    public async Task<Book> UpdateBookAsync(UpdateBookDto updateBookDto, CancellationToken cancellationToken = default)
    {
        var book = Mapper.Map<Book>(updateBookDto);

        if (book.PictureUrl.Length > 0)
        {
            _ = Task.Run(() => DeleteBookPictureAsync(book.Id, cancellationToken), cancellationToken);
        }

        book.PictureUrl = await CreateBookPictureAsync(book.Id, updateBookDto.Picture, cancellationToken);

        EfRepository.Books.Update(book);

        return book;
    }

    public Task<int> DeleteBookAsync(string bookId, CancellationToken cancellationToken = default)
    {
        return EfRepository
            .Books
            .Where(b => b.Id == bookId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}