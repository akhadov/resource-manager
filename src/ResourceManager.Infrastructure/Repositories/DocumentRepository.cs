using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Documents;
using ResourceManager.Infrastructure.Database;

namespace ResourceManager.Infrastructure.Repositories;

internal sealed class DocumentRepository(ApplicationDbContext context) : IDocumentRepository
{
    public async Task<IEnumerable<Document>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Documents.ToListAsync(cancellationToken);
    }

    public Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Documents.Include(h => h.Histories).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(Document document)
    {
        context.Documents.Add(document);
    }

    public void Update(Document document)
    {
       

        context.Documents.Update(document);
    }

    public void Delete(Document document)
    {
        context.Documents.Remove(document);
    }
}
