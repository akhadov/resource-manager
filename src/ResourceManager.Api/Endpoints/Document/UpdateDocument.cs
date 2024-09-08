using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.UpdateDocument;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class UpdateDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("documents/{documentId}", async (
            Guid documentId,
            UpdateDocumentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdateDocumentCommand(
                documentId,
                request.Title,
                request.Content);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
