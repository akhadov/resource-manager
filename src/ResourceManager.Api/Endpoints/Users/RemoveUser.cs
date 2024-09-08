using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Users.DeleteUser;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Users;

public class RemoveUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("users/{userId}", async (
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var user = new DeleteUserCommand(userId);

            Result result = await sender.Send(user, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
