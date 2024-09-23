using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.Approve;
using ResourceManager.Domain.Workflows;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class ApproveDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("documents/{documentId}/approve/{userId}/workflows{workflowId}", async (
            Guid documentId,
            Guid userId,
            Guid workflowId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new ApproveDocumentCommand(documentId, userId, workflowId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
