using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class CreateDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("documents/users/{creatorId}", async (
            Guid creatorId,
            CreateDocumentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateDocumentCommand(
                creatorId,
                request.Title,
                request.Content);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
