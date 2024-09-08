using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.GetDocuments;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class GetDocuments : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("documents", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            Result<IEnumerable<DocumentResponse>> result = await sender.Send(new GetDocumentsQuery(), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
