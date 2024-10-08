﻿using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;


namespace ResourceManager.Application.Documents.Reject;

internal sealed class RejectDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RejectDocumentCommand>
{
    public async Task<Result> Handle(RejectDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetWorkflowsAsync(request.DocumentId, cancellationToken);

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        document.Reject(user.Id, $"{request.Reason}", dateTimeProvider.UtcNow);

        var workflow = document.Workflows.Find(x => x.ApproverLevel == user.Level);

        workflow.MarkAsRejected();

        documentRepository.Update(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
