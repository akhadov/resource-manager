
using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Workflows;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Workflows.Remove;

internal sealed class RemoveWorkflowCommandHandler(
    IDocumentRepository documentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveWorkflowCommand>
{
    public async Task<Result> Handle(RemoveWorkflowCommand request, CancellationToken cancellationToken)
    {
        Document? document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        Workflow? workflow = document.Workflows.Find(w => w.Id == request.WorkflowId);

        document.RemoveWorkflow(workflow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
