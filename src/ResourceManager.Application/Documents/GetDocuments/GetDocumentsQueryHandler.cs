using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.GetDocuments
{
    internal sealed class GetDocumentsQueryHandler(
        IDocumentRepository documentRepository,
        IUserRepository userRepository)
        : IQueryHandler<GetDocumentsQuery, IEnumerable<DocumentResponse>>
    {
        public async Task<Result<IEnumerable<DocumentResponse>>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await documentRepository.GetAllAsync(cancellationToken);

            var users = await userRepository.GetAllAsync(cancellationToken);

            var documentResponses = from document in documents
                                    join user in users on document.CreatorId equals user.Id
                                    select new DocumentResponse(
                                        document.Id,
                                        document.CreatorId,
                                        user.Username,
                                        document.Title,
                                        document.Content,
                                        document.Status,
                                        document.CreatedAt,
                                        document.UpdatedAt);

            return documentResponses.ToList();
        }
    }
}
