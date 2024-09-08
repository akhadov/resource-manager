namespace ResourceManager.Application.Documents.CreateDocument;

public sealed record CreateDocumentRequest(
    string Title,
    string Content);
