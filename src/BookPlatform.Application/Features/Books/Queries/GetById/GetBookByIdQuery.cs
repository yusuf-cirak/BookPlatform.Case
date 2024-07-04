using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.Books.Dtos;
using BookPlatform.Application.Features.Books.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.Books.Queries.GetById;

public sealed record GetBookByIdQueryRequest(string Id) : IRequest<Result<GetBookDto>>;

public sealed class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQueryRequest, Result<GetBookDto>>
{
    private readonly IBookService _bookService;
    private readonly BaseService _baseService;

    public GetBookByIdQueryHandler(IBookService bookService, BaseService baseService)
    {
        _bookService = bookService;
        _baseService = baseService;
    }

    public async Task<Result<GetBookDto>> Handle(GetBookByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetBookByIdAsync(request.Id, cancellationToken);

        return book is not null
            ? (_baseService.Mapper.Map<GetBookDto>(book))
            : BookErrors.NotFound;
    }
}