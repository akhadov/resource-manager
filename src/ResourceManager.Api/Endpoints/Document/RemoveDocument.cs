using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.RemoveDocument;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class RemoveDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("documents/{documentId}", async (
            Guid documentId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new RemoveDocumentCommand(documentId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
