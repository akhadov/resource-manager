using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.UpdateDocument;

public sealed record UpdateDocumentCommand(
    Guid DocumentId,
    string Title,
    string Content) : ICommand<Guid>;
