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
        app.MapGet("documents/{documentId}/users/{userId}", async (
            Guid documentId,
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<DocumentResponse> result = await sender.Send(new GetDocumentQuery(documentId, userId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
