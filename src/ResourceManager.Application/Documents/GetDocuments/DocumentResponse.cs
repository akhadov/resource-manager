using ResourceManager.Domain.Documents;

namespace ResourceManager.Application.Documents.GetDocuments;

public sealed record DocumentResponse(
    Guid Id,
    Guid CreatorId,
    string Title,
    string Content,
    DocumentStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);