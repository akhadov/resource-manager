namespace ResourceManager.Domain.Documents;

public interface IDocumentHistoryRepository
{
    Task<IEnumerable<DocumentHistory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DocumentHistory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(DocumentHistory documentHistory);
    void Update(DocumentHistory documentHistory);
    void Delete(DocumentHistory documentHistory);
}
