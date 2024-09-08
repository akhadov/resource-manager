using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Documents;
using ResourceManager.Infrastructure.Database;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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
        foreach (var history in document.Histories)
        {
            var trackedEntity = context.ChangeTracker.Entries<DocumentHistory>()
                                       .FirstOrDefault(e => e.Entity.Id == history.Id);

            if (trackedEntity == null)
            {
                context.Histories.Attach(history);
                context.Entry(history).State = EntityState.Modified;
            }
            else
            {
                trackedEntity.State = EntityState.Modified;
            }
        }

        try
        {
            context.Documents.Update(document);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete(Document document)
    {
        context.Documents.Remove(document);
    }
}
