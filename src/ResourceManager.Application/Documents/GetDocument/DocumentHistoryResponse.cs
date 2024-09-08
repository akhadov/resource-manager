using ResourceManager.Domain.Documents;

namespace ResourceManager.Application.Documents.GetDocument;

public sealed record DocumentHistoryResponse(
    Guid DocumentId,
    Guid UserId,
    string Action,
    HistoryType Type,
    DateTime CreatedAt);
