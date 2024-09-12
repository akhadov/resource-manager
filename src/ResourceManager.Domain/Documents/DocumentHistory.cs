using ResourceManager.SharedKernel;

namespace ResourceManager.Domain.Documents;

public sealed class DocumentHistory : Entity
{
    private DocumentHistory(
        Guid documentId,
        Guid userId,
        string action,
        HistoryType type,
        DateTime createdAt)
    {
        DocumentId = documentId;
        UserId = userId;
        Action = action;
        Type = type;
        CreatedAt = createdAt;
    }

    private DocumentHistory() { }

    public Guid DocumentId { get; private set; }
    public Guid UserId { get; private set; }
    public string Action { get; private set; }
    public HistoryType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static DocumentHistory Create(
        Guid documentId,
        Guid userId,
        string action,
        HistoryType type,
        DateTime createdAt)
    {
        return new DocumentHistory(documentId, userId, action, type, createdAt);
    }

}