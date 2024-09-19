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
    //public Level? NextApproverLevel { get; private set; }
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
        //CurrentApproverLevel = _workflows[_currentApproverIndex].ApproverLevel;

        AddHistory(userId, "Submitted for approval", HistoryType.StatusChange, createdAt);
    }

    public void Approve(Guid approverId, Level approverLevel, DateTime updatedAt)
    {
        var currentWorkflow = _workflows[_currentApproverIndex];

        _currentApproverIndex++;

        if (_currentApproverIndex < _workflows.Count)
        {
            CurrentApproverLevel = _workflows[_currentApproverIndex].ApproverLevel;
        }
        else
        {
            Status = DocumentStatus.Approved;
            CurrentApproverLevel = null; // Reset since all approvals are done
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