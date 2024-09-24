using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.Reject;

public sealed record RejectDocumentCommand(
    Guid DocumentId,
    Guid UserId,
    Guid WorkflowId,
    string Reason) : ICommand;
