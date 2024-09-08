namespace ResourceManager.Application.Documents.UpdateDocument;

public sealed record UpdateDocumentRequest(
    string Title,
    string Content);
