using Microsoft.EntityFrameworkCore;
using ResourceManager.Domain.Documents;
using ResourceManager.Infrastructure.Database;

namespace ResourceManager.Infrastructure.Repositories;

internal sealed class DocumentHistoryRepository(ApplicationDbContext context) : IDocumentHistoryRepository
{
    public async Task<List<DocumentHistory>> GetAllByDocumentIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        return await context.Histories
            .Where(h => h.DocumentId == documentId)
            .ToListAsync(cancellationToken);
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
