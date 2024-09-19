using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.CreateDocument;

public sealed record CreateDocumentCommand(
    Guid CreatorId,
    string Title,
    string Content) : ICommand<Guid>;
