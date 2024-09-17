using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.GetDocument;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class GetDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("documents/{documentId}", async (
            Guid documentId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<DocumentResponse> result = await sender.Send(new GetDocumentQuery(documentId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
