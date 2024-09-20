using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Workflows.Remove;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class RemoveWorkflow : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("documents/{documentId}/workflows/{workflowId}", async (
            Guid documentId,
            Guid workflowId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new RemoveWorkflowCommand(documentId, workflowId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
