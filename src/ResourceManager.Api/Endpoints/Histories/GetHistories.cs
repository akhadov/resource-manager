using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.DocumentHistories.GetDocumentHistory;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Histories;

public class GetHistories : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("documents/{documentId}/histories", async (
            Guid documentId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<List<HistoryResponse>> result = await sender.Send(new GetDocumentHistoryQuery(documentId), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
