namespace ResourceManager.Domain.Documents;

public interface IDocumentRepository
{
    Task<IEnumerable<Document>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Document?> GetWorkflowsAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Document document);
    void Update(Document document);
    void Delete(Document document);
}
