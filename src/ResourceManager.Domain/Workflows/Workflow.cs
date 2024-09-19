using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Domain.Workflows;

public sealed class Workflow : Entity
{
    private Workflow(
        Guid Id,
        Guid documentId, 
        Level approverLevel)
    {
        DocumentId = documentId;
        ApproverLevel = approverLevel;
    }

    private Workflow() { }

    public Guid DocumentId { get; private set; }
    public Level ApproverLevel { get; private set; }
    public bool IsCurrentWorkflow { get; private set; }

    public static Result<Workflow> Create(
        Guid documentId,
        Level approverLevel)
    {
        var workklow = new Workflow(
            Guid.NewGuid(),
            documentId,
            approverLevel);

        return workklow;
    }

    public void MarkAsCurrent()
    {
        IsCurrentWorkflow = true;
    }
}