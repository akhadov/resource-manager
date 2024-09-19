using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.SubmitForApproval;

public sealed record SubmitForApprovalCommand(Guid DocumentId, Guid UserId) : ICommand;
