using ResourceManager.Domain.Documents;

namespace ResourceManager.Application.Documents.GetDocument;

public sealed record DocumentResponse(
    Guid Id,
    Guid CreatorId,
    string Title,
    string Content,
    DocumentStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public List<DocumentHistoryResponse> DocumentHistories { get; set;  } = [];
}
