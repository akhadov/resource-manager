using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Workflows;
using ResourceManager.Infrastructure.Database;

namespace ResourceManager.Infrastructure.Repositories;

internal sealed class WorkflowRepository(ApplicationDbContext context) : IWorkflowRepository
{
    public Task<Workflow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Workflows.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public void Insert(Workflow workflow)
    {
        context.Workflows.Add(workflow);
    }

    public void Remove(Workflow workflow)
    {
        context.Workflows.Remove(workflow);
    }
}
