using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.Application.Documents.UpdateDocument;
using ResourceManager.UI.Services.Interfaces;

namespace ResourceManager.UI.Services;

public class DocumentService : IDocumentService
{
    public Task<Application.Documents.GetDocument.DocumentResponse> AddFolder(CreateDocumentRequest document)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDocument(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Application.Documents.GetDocument.DocumentResponse> GetDocument(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Application.Documents.GetDocuments.DocumentResponse>?> GetDocuments()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDocument(Guid documentId, string title, string content)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDocuments(Guid documentId, UpdateDocumentRequest updateDocumentRequest)
    {
        throw new NotImplementedException();
    }
}
