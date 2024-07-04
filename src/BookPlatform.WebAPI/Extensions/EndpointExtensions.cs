using BookPlatform.WebAPI.Endpoints;

namespace BookPlatform.WebAPI.Extensions;

public static class EndpointExtensions
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapUserEndpoints();
        app.MapUserFriendEndpoints();
        // app.MapBookEndpoints();
        app.MapBookNoteEndpoints();

        app.MapControllers();

    }
}