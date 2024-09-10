using ResourceManager.Domain.Documents;

namespace ResourceManager.Application.DocumentHistories.GetDocumentHistory;

public sealed record HistoryResponse(
    Guid DocumentId,
    Guid UserId,
    string Name,
    string Action,
    HistoryType Type,
    DateTime CreatedAt);
