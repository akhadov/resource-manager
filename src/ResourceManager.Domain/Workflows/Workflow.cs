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
        IsChecked = false;
    }

    private Workflow() { }

    public Guid DocumentId { get; private set; }
    public Level ApproverLevel { get; private set; }
    public bool IsApproved { get; private set; }
    public bool IsChecked { get; private set; }

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

    public void MarkAsApproved()
    {
        IsApproved = true;
        IsChecked = true;
    }

    public void MarkAsRejected()
    {
        IsApproved = false;
        IsChecked = true;
    }

}