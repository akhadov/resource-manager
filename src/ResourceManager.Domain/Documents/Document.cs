using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Domain.Documents;

public sealed class Document : Entity
{
    private readonly List<DocumentHistory> _histories = new();

    private Document(
        Guid id,
        Guid creatorId,
        string title,
        string content,
        DocumentStatus status,
        DateTime createdAt)
        : base(id)
    {
        CreatorId = creatorId;
        Title = title;
        Content = content;
        Status = status;
        CreatedAt = createdAt;
    }

    private Document() { }

    public Guid CreatorId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DocumentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Level? CurrentApproverLevel { get; private set; }
    public List<DocumentHistory> Histories => _histories.ToList();

    public static Document Create(
        Guid creatorId,
        string title,
        string content,
        DateTime createdAt)
    {
        var document = new Document(
            Guid.NewGuid(),
            creatorId,
            title,
            content,
            DocumentStatus.Draft,
            createdAt);
        document.AddHistory(creatorId, "Document created", HistoryType.Creation, createdAt);
        return document;
    }

    public void Update(Guid creatorId, string title, string content, DateTime updatedAt)
    {
        if (Status != DocumentStatus.Draft)
        {
            throw new Exception("Only draft documents can be updated.");
        }

        if (creatorId != CreatorId)
        {
            throw new Exception("Only the document creator can update the document.");
        }

        Title = title;
        Content = content;
        UpdatedAt = updatedAt;

        AddHistory(creatorId, "Document updated", HistoryType.Update, updatedAt);
    }


    public void SubmitForApproval(Guid userId, Level userLevel, DateTime updatedAt)
    {
        if (Status != DocumentStatus.Draft)
            throw new InvalidOperationException("Only draft documents can be submitted for approval.");

        Status = DocumentStatus.PendingApproval;
        CurrentApproverLevel = userLevel;
        UpdatedAt = updatedAt;
        AddHistory(userId, "Submitted for approval", HistoryType.StatusChange, updatedAt);
    }

    public void Approve(Guid approverId, Level approverLevel, Level? nextApproverLevel, DateTime updatedAt)
    {
        if (Status != DocumentStatus.PendingApproval)
            throw new InvalidOperationException("Only pending documents can be approved.");

        if (approverLevel != CurrentApproverLevel)
            throw new InvalidOperationException("Approver does not have the required level for this step.");

        UpdatedAt = updatedAt;
        AddHistory(approverId, $"Document approved by {approverLevel}", HistoryType.Approval, updatedAt);

        if (nextApproverLevel == null)
        {
            Status = DocumentStatus.Approved;
            CurrentApproverLevel = null;
        }
        else
        {
            CurrentApproverLevel = nextApproverLevel;
        }
    }

    public void Reject(Guid rejectorId, string reason, DateTime updatedAt)
    {
        if (Status != DocumentStatus.PendingApproval)
            throw new InvalidOperationException("Only pending documents can be rejected.");

        Status = DocumentStatus.Rejected;
        CurrentApproverLevel = null;
        UpdatedAt = updatedAt;
        AddHistory(rejectorId, $"Document rejected. Reason: {reason}", HistoryType.Rejection, updatedAt);
    }

    private void AddHistory(Guid userId, string action, HistoryType type, DateTime createdAt)
    {
        _histories.Add(DocumentHistory.Create(Id, userId, action, type, createdAt));
    }

}