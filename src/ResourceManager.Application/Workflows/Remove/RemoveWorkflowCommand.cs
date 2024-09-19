using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Workflows.Remove;

public sealed record RemoveWorkflowCommand(Guid DocumentId, Guid WorkflowId) : ICommand;
