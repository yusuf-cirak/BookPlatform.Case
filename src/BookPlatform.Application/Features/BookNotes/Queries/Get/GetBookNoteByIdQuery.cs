using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Dtos;
using BookPlatform.Application.Features.BookNotes.Rules;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.BookNotes.Queries.Get;

public sealed record GetBookNoteByIdQueryRequest(string Id) : IRequest<Result<GetBookNoteDto>>;

public sealed class GetBookNoteByIdQueryHandler : IRequestHandler<GetBookNoteByIdQueryRequest, Result<GetBookNoteDto>>
{
    private readonly BaseService _baseService;
    private readonly IBookNoteService _bookNoteService;
    private readonly BookNoteBusinessRules _bookNoteBusinessRules;

    public GetBookNoteByIdQueryHandler(BaseService baseService, IBookNoteService bookNoteService,
        BookNoteBusinessRules bookNoteBusinessRules)
    {
        _baseService = baseService;
        _bookNoteService = bookNoteService;
        _bookNoteBusinessRules = bookNoteBusinessRules;
    }

    public async Task<Result<GetBookNoteDto>> Handle(GetBookNoteByIdQueryRequest request,
        CancellationToken cancellationToken)
    {
        var bookNote = await _bookNoteService.GetBookNoteByIdAsync(request.Id, cancellationToken);

        if (bookNote == null)
        {
            return BookNoteErrors.NotFound;
        }

        var canViewBookNote =
            _bookNoteBusinessRules.CanUserViewBookNote(_baseService.CurrentUser.User!, bookNote);

        if (canViewBookNote.IsFailure)
        {
            return canViewBookNote.Error;
        }

        var bookNoteDto = _baseService.Mapper.Map<GetBookNoteDto>(bookNote);

        return bookNoteDto;
    }
}