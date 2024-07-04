using BookPlatform.Application.Features.Books.Commands.Delete;
using BookPlatform.Application.Features.UserFriends.Commands;
using BookPlatform.Application.Features.UserFriends.Commands.Delete;
using BookPlatform.SharedKernel.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.WebAPI.Endpoints;


public static class UserFriendEndpoints
{
    public static void MapUserFriendEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("/user-friends")
            .WithTags("UserFriends");
        
        groupBuilder.MapPost("/",
            async ([FromBody] CreateUserFriendCommandRequest createUserFriendCommandRequest, IMediator mediator) =>
            (await mediator.Send(createUserFriendCommandRequest)).ToHttpResponse(201));
        
        groupBuilder.MapDelete("/{id}",
            async ([FromRoute] string id, IMediator mediator) =>
            (await mediator.Send(new DeleteUserFriendCommandRequest(id))).ToHttpResponse(204));
    }
}