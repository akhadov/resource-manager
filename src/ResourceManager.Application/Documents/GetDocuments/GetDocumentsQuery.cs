using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.GetDocuments;

public sealed record GetDocumentsQuery : IQuery<IEnumerable<DocumentResponse>>;
