using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.Books.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.Books.Commands.Delete;

public sealed record DeleteBookCommandRequest(string Id) : IRequest<Result>;


public sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommandRequest, Result>
{
    private readonly IBookService _bookService;
    private readonly BaseService _baseService;

    public DeleteBookCommandHandler(IBookService bookService, BaseService baseService)
    {
        _bookService = bookService;
        _baseService = baseService;
    }

    public async Task<Result> Handle(DeleteBookCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _bookService.DeleteBookAsync(request.Id, cancellationToken) > 0;

        return result switch
        {
            true => true,
            false => BookErrors.FailedToDelete
        };
    }
}