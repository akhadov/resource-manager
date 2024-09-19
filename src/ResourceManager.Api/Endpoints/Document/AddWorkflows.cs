using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Workflows.Create;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class AddWorkflows : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("documents/{documentId}/workflows", async (
            Guid documentId,
            List<WorkflowRequest> workflows,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateWorkflowCommand(documentId, workflows);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
