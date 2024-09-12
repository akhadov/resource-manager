using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Application.Abstractions.Storage;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.CreateDocument;

internal sealed class CreateDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IFileService fileService,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateDocumentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.CreatorId);

        if (user.Actor != Actor.Provider)
        {
            //return Result.Failure<Guid>("Only providers are allowed to create documents");
            throw new Exception("Only providers are allowed to create documents");
        }

        //string documentContent = await fileService.UploadAsync(request.Content, cancellationToken);

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
