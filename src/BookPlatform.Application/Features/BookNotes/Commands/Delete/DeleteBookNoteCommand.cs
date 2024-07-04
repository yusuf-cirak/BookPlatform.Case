using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Common.Services;
using BookPlatform.Application.Features.BookNotes.Rules;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Domain;
using MediatR;

namespace BookPlatform.Application.Features.BookNotes.Commands.Delete;

public sealed record DeleteBookNoteCommandRequest(string BookNoteId) : IRequest<Result> , ISecuredRequest;

public sealed class DeleteBookNoteCommandHandler : IRequestHandler<DeleteBookNoteCommandRequest,Result>
{
    private readonly BaseService _baseService;
    private readonly BookNoteBusinessRules _bookNoteBusinessRules;
    private readonly IBookNoteService _bookNoteService;

    public DeleteBookNoteCommandHandler(BookNoteBusinessRules bookNoteBusinessRules, BaseService baseService, IBookNoteService bookNoteService)
    {
        _bookNoteBusinessRules = bookNoteBusinessRules;
        _baseService = baseService;
        _bookNoteService = bookNoteService;
    }

    public async Task<Result> Handle(DeleteBookNoteCommandRequest request, CancellationToken cancellationToken)
    {
        var userId = _baseService.CurrentUser.GetUserId();
        
        var bookNoteResult = await _bookNoteBusinessRules.RequesterMustBeOwnerOfBookNoteAsync(userId, request.BookNoteId);
        
        if (bookNoteResult.IsFailure)
        {
            return bookNoteResult.Error;
        }
        
        var result = await _bookNoteService.DeleteBookNoteAsync(request.BookNoteId, cancellationToken) > 0;
        
        return result switch
        {
            true => true,
            false => BookNoteErrors.FailedToCreateBookNote
        };
    }
}