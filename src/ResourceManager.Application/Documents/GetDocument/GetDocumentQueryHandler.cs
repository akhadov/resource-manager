using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.GetDocument;

internal sealed class GetDocumentQueryHandler(
    IDocumentRepository documentRepository,
    IDocumentHistoryRepository documentHistoryRepository,
    IUserRepository userRepository)
    : IQueryHandler<GetDocumentQuery, DocumentResponse>
{
    public async Task<Result<DocumentResponse>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        var user = await userRepository.GetByIdAsync(document.CreatorId, cancellationToken);

        var documentHistoryResponses = document.Histories.Select(history => new DocumentHistoryResponse(
            history.DocumentId,
            history.UserId,
            user.Name,
            history.Action,
            history.Type,
            history.CreatedAt
        )).ToList();

        // Create the response with document details and its histories
        var result = new DocumentResponse(
            document.Id,
            document.CreatorId,
            user.Username,
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
