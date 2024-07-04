using BookPlatform.Application.Features.BookNotes.Commands.Create;
using BookPlatform.Application.Features.BookNotes.Commands.Update;
using BookPlatform.Application.Features.BookNotes.Queries.Get;
using BookPlatform.Application.Features.Books.Commands.Delete;
using BookPlatform.SharedKernel.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Endpoints;

public static class BookNoteEndpoints
{
    public static void MapBookNoteEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/book-notes")
            .WithTags("BookNotes");

        groupBuilder.MapGet("/{id}",
            async ([FromRoute] string id, IMediator mediator) =>
            (await mediator.Send(new GetBookNoteByIdQueryRequest(id))).ToHttpResponse(200));


        groupBuilder.MapPost("/",
            async ([FromBody] CreateBookNoteCommandRequest createBookNoteCommandRequest, IMediator mediator) =>
            (await mediator.Send(createBookNoteCommandRequest)).ToHttpResponse(201));

        groupBuilder.MapPut("/",
            async ([FromBody] UpdateBookNoteCommandRequest updateBookNoteCommandRequest, IMediator mediator) =>
            (await mediator.Send(updateBookNoteCommandRequest)).ToHttpResponse(200));

        groupBuilder.MapDelete("/{id}",
            async ([FromRoute] string id, IMediator mediator) =>
            (await mediator.Send(new DeleteBookCommandRequest(id))).ToHttpResponse(204));
    }
}