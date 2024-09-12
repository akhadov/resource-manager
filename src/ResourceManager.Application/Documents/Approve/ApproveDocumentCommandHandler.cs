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

        if (user.Actor != Actor.Approver)
        {
            throw new InvalidOperationException("Only approvers can approve documents.");
        }

        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);


        if (user.Level != document.NextApproverLevel)
        {
            throw new InvalidOperationException($"The document must be approved by the next approver: {document.NextApproverLevel}.");
        }

        document.Approve(user.Id, user.Level, dateTimeProvider.UtcNow);

        documentRepository.Update(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
