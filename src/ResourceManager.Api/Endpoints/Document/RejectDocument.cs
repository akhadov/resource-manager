﻿using MediatR;
using ResourceManager.Api.Extensions;
using ResourceManager.Api.Infrastructure;
using ResourceManager.Application.Documents.Reject;
using ResourceManager.SharedKernel;

namespace ResourceManager.Api.Endpoints.Document;

public class RejectDocument : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("documents/{documentId}/reject/{userId}", async (
            Guid documentId,
            Guid userId,
            RejectDocumentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new RejectDocumentCommand(
                documentId,
                userId,
                request.Reason);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Documents);
    }
}
