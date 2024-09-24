using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Users;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Documents.UpdateDocument;

internal sealed class UpdateDocumentCommandHandler(
    IDocumentRepository documentRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UpdateDocumentCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        var user = await userRepository.GetByIdAsync(document.CreatorId);

        if (user.Actor != Actor.Provider && document.CreatorId != user.Id)
        {
            throw new Exception("Only the document owner (Provider) can update the document.");
        }

        if (document.Status == DocumentStatus.Rejected)
        {
            var workflows = await documentRepository.GetWorkflowsAsync(request.DocumentId);

            foreach (var workflow in workflows.Workflows)
            {
                document.RemoveWorkflow(workflow);
            }
        }

        document.Update(
            document.CreatorId,
            request.Title,
            request.Content,
            dateTimeProvider.UtcNow);


        documentRepository.Update(document);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return document.Id;
    }
}
