using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Users.GetUser;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Users;

public class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(userId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
