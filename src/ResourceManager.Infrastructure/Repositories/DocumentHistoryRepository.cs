using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Documents;
using ResourceManager.Infrastructure.Database;

namespace ResourceManager.Infrastructure.Repositories;

internal sealed class DocumentHistoryRepository(ApplicationDbContext context) : IDocumentHistoryRepository
{
    public async Task<IEnumerable<DocumentHistory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Histories.ToListAsync(cancellationToken);
    }

    public Task<DocumentHistory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Histories.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public void Insert(DocumentHistory documentHistory)
    {
        context.Histories.Add(documentHistory);
    }

    public void Update(DocumentHistory documentHistory)
    {
        context.Histories.Update(documentHistory);
    }

    public void Delete(DocumentHistory documentHistory)
    {
        context.Histories.Remove(documentHistory);
    }
}
