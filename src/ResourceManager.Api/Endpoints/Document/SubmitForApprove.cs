using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.SubmitForApproval;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class SubmitForApprove : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("documents/{documentId}/submit/{userId}", async (
            Guid documentId,
            Guid userId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result result = await sender.Send(new SubmitForApprovalCommand(documentId, userId), cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
