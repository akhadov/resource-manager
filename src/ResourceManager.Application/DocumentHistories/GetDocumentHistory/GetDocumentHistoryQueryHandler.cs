using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.DocumentHistories.GetDocumentHistory;

internal sealed class GetDocumentHistoryQueryHandler(
    IDocumentHistoryRepository documentHistoryRepository,
    IUserRepository userRepository) : IQueryHandler<GetDocumentHistoryQuery, List<HistoryResponse>>
{
    public async Task<Result<List<HistoryResponse>>> Handle(GetDocumentHistoryQuery request, CancellationToken cancellationToken)
    {
        var histories = await documentHistoryRepository.GetAllByDocumentIdAsync(request.DocumentId, cancellationToken);

        var users = await userRepository.GetAllAsync(cancellationToken);

        var historyResponses = from history in histories
                               join user in users on history.UserId equals user.Id
                               select new HistoryResponse(
                                   history.DocumentId,
                                   history.UserId,
                                   user.Name,
                                   history.Action,
                                   history.Type,
                                   history.CreatedAt);

        return historyResponses.ToList();
    }
}
