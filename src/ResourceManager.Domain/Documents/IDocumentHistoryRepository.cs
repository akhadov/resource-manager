namespace ResourceManager.Domain.Documents;

public interface IDocumentHistoryRepository
{
    Task<List<DocumentHistory>> GetAllByDocumentIdAsync(Guid documentId, CancellationToken cancellationToken = default);
    void Insert(DocumentHistory documentHistory);
    void Update(DocumentHistory documentHistory);
    void Delete(DocumentHistory documentHistory);
}
