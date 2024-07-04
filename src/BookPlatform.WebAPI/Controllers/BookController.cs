using BookPlatform.Application.Features.Books.Commands.Create;
using BookPlatform.Application.Features.Books.Commands.Delete;
using BookPlatform.Application.Features.Books.Commands.Update;
using BookPlatform.Application.Features.Books.Queries.GetByFilter;
using BookPlatform.Application.Features.Books.Queries.GetById;
using BookPlatform.SharedKernel.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Controllers;

[ApiController]
[Route("_api/books")]
public sealed class BookController : Controller
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetBookById([FromRoute] string id)
    {
        var result = await _mediator.Send(new GetBookByIdQueryRequest(id));
        return result.ToHttpResponse(200);
    }

    [HttpGet]
    public async Task<IResult> GetBooksByFilter([FromQuery] string filter)
    {
        var result = await _mediator.Send(new GetBooksByFilterQueryRequest(filter));
        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<IResult> CreateBook([FromForm] CreateBookCommandRequest createBookCommandRequest)
    {
        var result = await _mediator.Send(createBookCommandRequest);
        return result.ToHttpResponse(201);
    }

    [HttpPut]
    public async Task<IResult> UpdateBook([FromForm] UpdateBookCommandRequest updateBookCommandRequest)
    {
        var result = await _mediator.Send(updateBookCommandRequest);
        return result.ToHttpResponse(200);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteBook([FromRoute] string id)
    {
        var result = await _mediator.Send(new DeleteBookCommandRequest(id));
        return result.ToHttpResponse(204);
    }
}