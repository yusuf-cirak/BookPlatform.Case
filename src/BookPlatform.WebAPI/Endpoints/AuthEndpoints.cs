using Application.Features.Auths.Commands.Register;
using BookPlatform.Application.Features.Auths.Commands.Login;
using BookPlatform.Application.Features.Auths.Commands.Register;
using BookPlatform.SharedKernel.Extensions;
using MediatR;

namespace BookPlatform.WebAPI.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/auths");


        groupBuilder.MapPost("/register",
                async (RegisterCommandRequest registerCommandRequest, IMediator mediator) =>
                {
                    return (await mediator.Send(registerCommandRequest)).ToHttpResponse(201);
                })
            .WithTags("Auths");


        groupBuilder.MapPost("/login",
                async (LoginCommandRequest loginCommandRequest, IMediator mediator) =>
                {
                    return (await mediator.Send(loginCommandRequest)).ToHttpResponse(200);
                })
            .WithTags("Auths");
    }
}