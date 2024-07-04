using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Dtos;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Domain;
using BookPlatform.Domain.Enums;
using MediatR;

namespace BookPlatform.Application.Features.BookNotes.Commands.Create;

public sealed record CreateBookNoteCommandRequest(string BookId, string Note, ShareType ShareType)
    : IRequest<Result<GetBookNoteDto>>, ISecuredRequest;

public sealed class CreateBookNoteCommandHandler : IRequestHandler<CreateBookNoteCommandRequest, Result<GetBookNoteDto>>
{
    private readonly IBookNoteService _bookNoteService;
    private readonly BaseService _baseService;

    public CreateBookNoteCommandHandler(IBookNoteService bookNoteService, BaseService baseService)
    {
        _bookNoteService = bookNoteService;
        _baseService = baseService;
    }

    public async Task<Result<GetBookNoteDto>> Handle(CreateBookNoteCommandRequest request,
        CancellationToken cancellationToken)
    {
        var uow = _baseService.UnitOfWork;

        var currentUserId = _baseService.CurrentUser.GetUserId();

        var bookNote = _bookNoteService.CreateBookNote(currentUserId, request.BookId, request.Note, request.ShareType);

        var saved = await uow.SaveChangesAsync(cancellationToken) > 0;

        return saved switch
        {
            true => _baseService.Mapper.Map<GetBookNoteDto>(bookNote),
            false => BookNoteErrors.FailedToCreateBookNote
        };
    }
}