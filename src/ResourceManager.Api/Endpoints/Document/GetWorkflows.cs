using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Workflows.GetById;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class GetWorkflows : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("documents/{documentId}/workflows", async (
            Guid documentId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<List<WorkflowResponse>> result = await sender.Send(new GetWorkflowByIdQuery(documentId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
