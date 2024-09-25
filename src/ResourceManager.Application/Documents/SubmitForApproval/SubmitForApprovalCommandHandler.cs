using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.SubmitForApproval;

internal sealed class SubmitForApprovalCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SubmitForApprovalCommand>
{
    public async Task<Result> Handle(SubmitForApprovalCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetWorkflowsAsync(request.DocumentId, cancellationToken);

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (document.CreatorId != user.Id)
        {
            throw new Exception("Only author can submit for approving");
        }

        document.SubmitForApproval(document.CreatorId, dateTimeProvider.UtcNow);

        var firstWorkflow = document.Workflows.FirstOrDefault();
        //if (firstWorkflow != null)
        //{
        //    document.CurrentApproverLevel = firstWorkflow.ApproverLevel;
        //}
        //else
        //{
        //    throw new InvalidOperationException("No workflow exists for this document.");
        //}

        documentRepository.Update(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
