﻿using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.Application.Documents.GetDocument;
using ResourceManager.Application.Documents.GetDocuments;
using ResourceManager.Application.Documents.UpdateDocument;

namespace ResourceManager.UI.Services.Interfaces;

public interface IDocumentService
{
    Task<List<Application.Documents.GetDocuments.DocumentResponse>?> GetDocuments();
    Task<Application.Documents.GetDocument.DocumentResponse> GetDocument(Guid id);
    Task<Application.Documents.GetDocument.DocumentResponse> AddFolder(CreateDocumentRequest document);
    Task<bool> UpdateDocuments(Guid documentId, UpdateDocumentRequest updateDocumentRequest);
    Task<bool> UpdateDocument(Guid documentId, string title, string content);
    Task<bool> DeleteDocument(Guid id);
}
