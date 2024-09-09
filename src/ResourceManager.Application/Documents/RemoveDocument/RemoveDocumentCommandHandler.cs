using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.RemoveDocument;

internal sealed class RemoveDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IDocumentHistoryRepository documentHistoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveDocumentCommand>
{
    public async Task<Result> Handle(RemoveDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        documentRepository.Delete(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
