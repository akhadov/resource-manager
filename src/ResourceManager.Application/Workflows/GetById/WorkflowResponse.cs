using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Workflows.GetById;

public sealed record WorkflowResponse(
    Guid WorkflowId,
    Level ApproverLevel,
    bool IsApproved,
    bool IsChecked);