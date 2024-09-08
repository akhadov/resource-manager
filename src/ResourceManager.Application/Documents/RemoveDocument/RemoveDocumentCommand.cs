using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.RemoveDocument;

public sealed record RemoveDocumentCommand(Guid DocumentId) : ICommand;
