using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.CreateDocument;

internal sealed class CreateDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateDocumentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.CreatorId);

        if (user.Actor != Actor.Provider)
        {
            throw new Exception("Only providers are allowed to create documents");
        }


        var document = Document.Create(
            request.CreatorId,
            request.Title,
            request.Content,
            dateTimeProvider.UtcNow);

        documentRepository.Insert(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}
