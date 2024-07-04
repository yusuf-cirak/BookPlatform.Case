using BookPlatform.Application.Features.Users.Commands.UpdateUsername;
using BookPlatform.SharedKernel.Extensions;
using MediatR;

namespace BookPlatform.WebAPI.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/users");


        groupBuilder.MapPut("/",
                async (UpdateUsernameCommandRequest usernameCommandRequest, IMediator mediator) =>
                {
                    return (await mediator.Send(usernameCommandRequest)).ToHttpResponse();
                })
            .WithTags("Users");
    }
}