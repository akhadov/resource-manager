using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Users.GetUser;
using ResourceManager.Application.Users.GetUsers;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Users;

public class GetUsers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<IEnumerable<UserResponse>> result = await sender.Send(new GetUsersQuery(), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
