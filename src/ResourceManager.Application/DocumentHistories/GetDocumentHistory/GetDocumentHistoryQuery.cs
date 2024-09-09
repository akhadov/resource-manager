using ResourceManager.Application.Abstractions.Messaging;

namespace ResourceManager.Application.DocumentHistories.GetDocumentHistory;

public sealed record GetDocumentHistoryQuery(Guid DocumentId) : IQuery<List<HistoryResponse>>;
