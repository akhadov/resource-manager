using ResourceManager.Application.DocumentHistories.GetDocumentHistory;
using ResourceManager.Application.Documents.CreateDocument;
using ResourceManager.Application.Documents.Reject;
using ResourceManager.Application.Documents.UpdateDocument;
using ResourceManager.Application.Workflows.Create;
using ResourceManager.Application.Workflows.GetById;

namespace ResourceManager.UI.Services.Interfaces;

public interface IDocumentService
{
    Task<List<Application.Documents.GetDocuments.DocumentResponse>?> GetDocuments();
    Task<Application.Documents.GetDocument.DocumentResponse> GetDocument(Guid id);
    //Task<Application.Documents.GetDocuments.DocumentResponse> AddDocument(Guid userId, CreateDocumentRequest document);
    Task<bool> AddDocument(Guid userId, CreateDocumentRequest document);
    Task<bool> UpdateDocuments(Guid documentId, UpdateDocumentRequest updateDocumentRequest);
    Task<bool> DeleteDocument(Guid id);
    Task<List<HistoryResponse>> GetHistories(Guid documentId);
    Task<bool> AddWorkflow(Guid documentId, WorkflowRequest workflow);
    Task<bool> RemoveWorkflow(Guid documentId, Guid workflowId);
    Task<List<WorkflowResponse>> GetWorkflows(Guid documentId);
    Task<bool> SubmitForApproval(Guid documentId, Guid userId);
    Task<bool> ApproveDocument(Guid documentId, Guid userId, Guid workflowId);
    Task<bool> RejectDocument(Guid documentId, Guid userId, Guid workflowId, RejectDocumentRequest reason);
}
