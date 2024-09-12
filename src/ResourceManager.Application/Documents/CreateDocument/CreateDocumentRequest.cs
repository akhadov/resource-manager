using Microsoft.AspNetCore.Http;

namespace ResourceManager.Application.Documents.CreateDocument;

public sealed record CreateDocumentRequest(
    string Title,
    string Content);
