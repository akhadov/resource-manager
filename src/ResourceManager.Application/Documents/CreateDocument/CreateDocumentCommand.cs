using Microsoft.AspNetCore.Http;
using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.Documents.CreateDocument;

public sealed record CreateDocumentCommand(
    Guid CreatorId,
    string Title,
    //IFormFile Content,
    string Content) : ICommand<Guid>;
