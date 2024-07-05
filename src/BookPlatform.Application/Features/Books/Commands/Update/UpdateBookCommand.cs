using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Rules;
using BookPlatform.Application.Features.Books.Dtos;
using BookPlatform.Application.Features.Books.Services;
using BookPlatform.Domain;
using BookPlatform.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookPlatform.Application.Features.Books.Commands.Update;

public sealed record UpdateBookCommandRequest : IRequest<Result<GetBookDto>>, ISecuredRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; }
    public ShelfLocation ShelfLocation { get; set; }
    public IFormFile? Picture { get; set; }
};

public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommandRequest, Result<GetBookDto>>
{
    private readonly IBookService _bookService;
    private readonly BaseService _baseService;

    public UpdateBookCommandHandler(IBookService bookService, BaseService baseService)
    {
        _bookService = bookService;
        _baseService = baseService;
    }

    public async Task<Result<GetBookDto>> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
    {
        var mapper = _baseService.Mapper;
        var uow = _baseService.UnitOfWork;
        
        var book = await _bookService.UpdateBookAsync(mapper.Map<UpdateBookDto>(request), cancellationToken);

        var result = await uow.SaveChangesAsync(cancellationToken) > 0;

        return result switch
        {
            true => mapper.Map<GetBookDto>(book),
            false => BookErrors.FailedToUpdate
        };
    }
}