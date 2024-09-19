using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;

namespace ResourceManager.Application.Documents.GetDocuments;

public sealed record DocumentResponse(
    Guid Id,
    Guid CreatorId,
    string Username,
    string Title,
    string Content,
    DocumentStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    Level? CurrentLevel);