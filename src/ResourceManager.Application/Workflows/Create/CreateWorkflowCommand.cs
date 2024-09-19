using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Workflows.Create;

public sealed record CreateWorkflowCommand(Guid DocumentId, List<WorkflowRequest> Workflows) : ICommand;
