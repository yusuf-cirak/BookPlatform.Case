using BookPlatform.Application.Features.Books.Commands.Create;
using BookPlatform.Application.Features.Books.Commands.Delete;
using BookPlatform.Application.Features.Books.Commands.Update;
using BookPlatform.Application.Features.Books.Queries.GetByFilter;
using BookPlatform.Application.Features.Books.Queries.GetById;
using BookPlatform.SharedKernel.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Endpoints;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/books")
            .WithTags("Books");


        groupBuilder.MapGet("/{id}",
            async ([FromRoute] string id, IMediator mediator) =>
            (await mediator.Send(new GetBookByIdQueryRequest(id))).ToHttpResponse(200));

        groupBuilder.MapGet("/",
            async ([FromQuery] string filter, IMediator mediator) =>
            (await mediator.Send(new GetBooksByFilterQueryRequest(filter))));

        groupBuilder.MapPost("/",
                async ([FromForm(Name = "book")] CreateBookCommandRequest createBookCommandRequest, [FromForm] IFormFile? picture,
                        IMediator mediator) =>
                    (await mediator.Send(createBookCommandRequest)).ToHttpResponse(201))
            .DisableAntiforgery();

        groupBuilder.MapPut("/",
                async ([FromForm] UpdateBookCommandRequest updateBookCommandRequest, IMediator mediator) =>
                (await mediator.Send(updateBookCommandRequest)).ToHttpResponse(200))
            .DisableAntiforgery();

        groupBuilder.MapDelete("/{id}",
            async ([FromRoute] string id, IMediator mediator) =>
            (await mediator.Send(new DeleteBookCommandRequest(id))).ToHttpResponse(204));
    }
}