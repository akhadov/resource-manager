using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.GetDocument;

public sealed record GetDocumentQuery(
    Guid DocumentId,
    Guid UserId) : IQuery<DocumentResponse>;
