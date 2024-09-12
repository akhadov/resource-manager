using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.Approve;

internal sealed class ApproveDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ApproveDocumentCommand>
{
    public async Task<Result> Handle(ApproveDocumentCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.ApproverId, cancellationToken);

        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        //if (user.Id == document.CreatorId) 
        //{
        //    throw new Exception("Authot can't approve document");
        //}

        document.Approve(user.Id, user.Level, Level.FinalApprover, dateTimeProvider.UtcNow);

        documentRepository.Update(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
