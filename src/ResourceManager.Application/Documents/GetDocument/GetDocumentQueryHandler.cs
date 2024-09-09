using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.GetDocument;

internal sealed class GetDocumentQueryHandler(
    IDocumentRepository documentRepository,
    IDocumentHistoryRepository documentHistoryRepository,
    IUserRepository userRepository) // Add repository for document histories
    : IQueryHandler<GetDocumentQuery, DocumentResponse>
{
    public async Task<Result<DocumentResponse>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the document
        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (document.Status == DocumentStatus.PendingApproval)
        {
            // User can view if their level is at or below the current approver level
            if (!document.CurrentApproverLevel.HasValue || user.Level <= document.CurrentApproverLevel.Value)
            {
                // Continue to create response if access is granted
            }
            else
            {
                throw new Exception("User does not have the required level to view this document.");
            }
        }
        else if (document.Status == DocumentStatus.Approved)
        {
            // Document is approved, so any user can view it
        }
        else
        {
            throw new Exception("Document status is not accessible.");
        }


        // Map the document histories to the response model
        var documentHistoryResponses = document.Histories.Select(history => new DocumentHistoryResponse(
            history.DocumentId,
            history.UserId,
            history.Action,
            history.Type,
            history.CreatedAt
        )).ToList();

        // Create the response with document details and its histories
        var result = new DocumentResponse(
            document.Id,
            document.CreatorId,
            document.Title,
            document.Content,
            document.Status,
            document.CreatedAt,
            document.UpdatedAt)
        {
            DocumentHistories = documentHistoryResponses
        };

        return result;
    }
}
