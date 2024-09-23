using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.Approve;

public sealed record ApproveDocumentCommand(
    Guid DocumentId,
    Guid ApproverId,
    Guid WorkflowId) : ICommand;
