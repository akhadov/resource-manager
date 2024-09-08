using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.GetDocuments;

internal sealed class GetDocumentsQueryHandler(
    IDocumentRepository documentRepository)
    : IQueryHandler<GetDocumentsQuery, IEnumerable<DocumentResponse>>
{
    public async Task<Result<IEnumerable<DocumentResponse>>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await documentRepository.GetAllAsync(cancellationToken);

        var result = documents.Select(
            d => new DocumentResponse(
                d.Id,
                d.CreatorId,
                d.Title,
                d.Content,
                d.Status,
                d.CreatedAt,
                d.UpdatedAt))
            .ToList();

        return result;
    }
}
