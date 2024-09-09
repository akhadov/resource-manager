using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.DocumentHistories.GetDocumentHistory;

internal sealed class GetDocumentHistoryQueryHandler(
    IDocumentHistoryRepository documentHistoryRepository,
    IDocumentRepository documentRepository) : IQueryHandler<GetDocumentHistoryQuery, List<HistoryResponse>>
{
    public async Task<Result<List<HistoryResponse>>> Handle(GetDocumentHistoryQuery request, CancellationToken cancellationToken)
    {
        var histories = await documentHistoryRepository.GetAllByDocumentIdAsync(request.DocumentId, cancellationToken);

        // Map to HistoryResponse
        var historyResponses = histories.Select(history => new HistoryResponse(
            history.DocumentId,
            history.UserId,
            history.Action,
            history.Type,
            history.CreatedAt
        )).ToList();

        return historyResponses;
    }
}
