using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.Approve;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class ApproveDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("documents/{documentId}/approve/{userId}", async (
            Guid documentId,
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new ApproveDocumentCommand(documentId, userId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
