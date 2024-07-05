using System.Security.Claims;
using BookPlatform.Application.Common.Abstractions;
using BookPlatform.Application.Extensions;
using BookPlatform.Application.Features.BookNotes.Services;
using BookPlatform.Domain;
using BookPlatform.Domain.Enums;

namespace BookPlatform.Application.Features.BookNotes.Rules;

public sealed class BookNoteBusinessRules(IBookNoteService bookNoteService) : BaseBusinessRules
{
    public async Task<Result<BookNote>> RequesterMustBeOwnerOfBookNoteAsync(string userId, string bookNoteId)
    {
        var bookNote = await bookNoteService.GetBookNoteByIdAsync(bookNoteId);

        if (bookNote is null)
        {
            return BookNoteErrors.NotFound;
        }

        if (bookNote.UserId != userId)
        {
            return BookNoteErrors.NotOwnerOfBookNote;
        }

        return bookNote;
    }

    public Result CanUserViewBookNote(ClaimsPrincipal user, BookNote bookNote)
    {
        if (IsBookNoteShareTypePublic(bookNote))
        {
            return Result.Success();
        }

        var userId = user?.GetUserId() ?? string.Empty;

        var isOwnerResult = IsUserOwnerOfBookNote(userId, bookNote);

        if (isOwnerResult.IsSuccess)
        {
            return Result.Success();
        }

        if (IsBookNoteShareTypePrivate(bookNote))
        {
            return BookNoteErrors.IsPrivate;
        }

        var isFriendOfOwnerResult = IsUserFriendOfOwnerOfBookNote(user, bookNote);

        if (isFriendOfOwnerResult.IsFailure)
        {
            return isFriendOfOwnerResult;
        }

        return Result.Success();
    }


    private bool IsBookNoteShareTypePublic(BookNote bookNote)
    {
        return bookNote.ShareType is ShareType.Public;
    }

    private bool IsBookNoteShareTypePrivate(BookNote bookNote)
    {
        return bookNote.ShareType is ShareType.Private;
    }

    private bool IsBookNoteShareTypeOnlyFriends(BookNote bookNote)
    {
        return bookNote.ShareType is ShareType.OnlyFriends;
    }


    private Result IsUserOwnerOfBookNote(string userId, BookNote bookNote)
    {
        if (bookNote.UserId != userId)
        {
            return BookNoteErrors.NotOwnerOfBookNote;
        }

        return Result.Success();
    }

    public Result IsUserFriendOfOwnerOfBookNote(ClaimsPrincipal user, BookNote bookNote)
    {
        if (!user.Claims.Any())
        {
            return BookNoteErrors.NotVisible;
        }

        var ownerId = bookNote.UserId;

        var isFriend = user
            .GetFriends()
            .Contains(ownerId);

        if (!isFriend)
        {
            return BookNoteErrors.NotFriendOfOwnerBookNote;
        }

        return Result.Success();
    }
}