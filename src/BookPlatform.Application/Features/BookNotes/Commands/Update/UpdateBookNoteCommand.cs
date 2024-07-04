using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Dtos;
using BookPlatform.Application.Features.BookNotes.Rules;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Domain;
using BookPlatform.Domain.Enums;
using MediatR;

namespace BookPlatform.Application.Features.BookNotes.Commands.Update;

public sealed record
    UpdateBookNoteCommandRequest(string BookNoteId, string Note, ShareType ShareType)
    : IRequest<Result>, ISecuredRequest;

public sealed class UpdateBookNoteCommandHandler : IRequestHandler<UpdateBookNoteCommandRequest, Result>
{
    private readonly BookNoteBusinessRules _bookNoteBusinessRules;
    private readonly BaseService _baseService;
    private readonly IBookNoteService _bookNoteService;

    public UpdateBookNoteCommandHandler(BookNoteBusinessRules bookNoteBusinessRules, BaseService baseService,
        IBookNoteService bookNoteService)
    {
        _bookNoteBusinessRules = bookNoteBusinessRules;
        _baseService = baseService;
        _bookNoteService = bookNoteService;
    }

    public async Task<Result> Handle(UpdateBookNoteCommandRequest request,
        CancellationToken cancellationToken)
    {
        var userId = _baseService.CurrentUser.GetUserId();

        var bookNoteResult =
            await _bookNoteBusinessRules.RequesterMustBeOwnerOfBookNoteAsync(userId, request.BookNoteId);

        if (bookNoteResult.IsFailure)
        {
            return bookNoteResult.Error;
        }

        var updateBookNoteDto = new UpdateBookNoteDto(request.BookNoteId, request.Note, request.ShareType);

        var result = await _bookNoteService.UpdateBookNoteAsync(updateBookNoteDto, cancellationToken) > 0;

        return result switch
        {
            true => true,
            false => BookNoteErrors.NotFound
        };
    }
}