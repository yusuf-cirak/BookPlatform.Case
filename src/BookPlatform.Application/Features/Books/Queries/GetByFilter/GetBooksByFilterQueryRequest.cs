using BookPlatform.Application.Features.Books.Dtos;
using BookPlatform.Application.Features.Books.Services;
using MediatR;

namespace BookPlatform.Application.Features.Books.Queries.GetByFilter;

public sealed record GetBooksByFilterQueryRequest(string Filter) : IRequest<List<GetBookDto>>;

public sealed class GetBooksByFilterQueryHandler : IRequestHandler<GetBooksByFilterQueryRequest, List<GetBookDto>>
{
    private readonly IBookService _bookService;

    public GetBooksByFilterQueryHandler(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<List<GetBookDto>> Handle(GetBooksByFilterQueryRequest request, CancellationToken cancellationToken)
    {
        var books = await _bookService.GetBooksByFilterAsync(request.Filter, cancellationToken);

        return books;
    }
}