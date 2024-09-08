using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Users.CreateUser;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Users;

public class CreateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (
            CreateUserRequest request,
            ISender sender,
            CancellationToken calcellationToken) =>
        {
            var command = new CreateUserCommand(
                request.Name,
                request.Username,
                request.Actor,
                request.Level);

            Result<Guid> result = await sender.Send(command, calcellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
