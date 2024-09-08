using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Users.UpdateUser;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Users;

public class UpdateUser : IEndpoint

{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{userId}", async (
            Guid userId,
            UpdateUserRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserCommand(
                userId,
                request.Name,
                request.Username,
                request.Actor,
                request.Level);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
