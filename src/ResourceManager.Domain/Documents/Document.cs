using ResourceManager.Domain.Users;
using ResourceManager.Domain.Workflows;
using ResourceManager.SharedKernel;

namespace ResourceManager.Domain.Documents;

public sealed class Document : Entity
{
    private readonly List<DocumentHistory> _histories = new();
    private readonly List<Workflow> _workflows = new();
    private int _currentApproverIndex = 0;

    private Document(
        Guid id,
        Guid creatorId,
        string title,
        string content,
        DocumentStatus status,
        DateTime createdAt)
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
    public List<Workflow> Workflows => _workflows.ToList();

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

    public void Update(Guid userId, string title, string content, DateTime updatedAt)
    {
        if (Status != DocumentStatus.Draft && Status != DocumentStatus.Rejected)
        {
            throw new InvalidOperationException("Only draft or rejected documents can be updated.");
        }

        if (userId != CreatorId)
        {
            throw new UnauthorizedAccessException("Only the document creator can update the document.");
        }

        Title = title;
        Content = content;
        UpdatedAt = updatedAt;

        if (Status == DocumentStatus.Rejected)
        {
            Status = DocumentStatus.Draft;

            foreach (var workflow in _workflows)
            {
                RemoveWorkflow(workflow);
            }
        }

        AddHistory(userId, "Document updated", HistoryType.Update, updatedAt);
    }


    public Result AddWorkflow(
        Level approverLevel)
    {
        Result<Workflow> result = Workflow.Create(
            Id,
            approverLevel);

        if (result.IsFailure)
        {
            return result;
        }

        _workflows.Add(result.Value);

        return Result.Success();
    }

    public void RemoveWorkflow(Workflow workflow)
    {
        _workflows.Remove(workflow);
    }

    public void ClearWorkflows()
    {
        _workflows.Clear();
    }


    public void SubmitForApproval(Guid userId, DateTime createdAt)
    {
        if (Status != DocumentStatus.Draft)
            throw new InvalidOperationException("Only draft documents can be submitted for approval.");

        Status = DocumentStatus.PendingApproval;

        AddHistory(userId, $"Document submitted for approval to {CurrentApproverLevel}", HistoryType.StatusChange, createdAt);
    }

    public void Approve(Guid approverId, Level approverLevel, DateTime updatedAt)
    {
        if (Status != DocumentStatus.PendingApproval)
            throw new InvalidOperationException("Document is not in a state that can be approved.");

        bool anyWorkflowRejected = _workflows.Any(w => !w.IsApproved && w.IsChecked);

        if (anyWorkflowRejected)
        {
            Status = DocumentStatus.Rejected;
        }
       
        bool allWorkflowsApprovedAndChecked = _workflows.All(w => w.IsApproved && w.IsChecked);

        if (allWorkflowsApprovedAndChecked)
        {
            Status = DocumentStatus.Approved;
        }
        else
        {
            Status = DocumentStatus.PendingApproval;
        }

        UpdatedAt = updatedAt;

        AddHistory(approverId, $"Document approved by {approverLevel}", HistoryType.Approval, updatedAt);
    }

    public void Reject(Guid rejectorId, string reason, DateTime updatedAt)
    {
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