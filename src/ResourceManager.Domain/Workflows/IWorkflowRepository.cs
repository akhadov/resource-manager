namespace ResourceManager.Domain.Workflows;

public interface IWorkflowRepository
{
    Task<Workflow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Workflow workflow);

    void Remove(Workflow workflow);
}
