using ResourceManager.Application.Abstractions.Data;
using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.Domain.Workflows;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Workflows.Create;

internal sealed class CreateWorkflowCommandHandler(
    IWorkflowRepository workflowRepository,
    IDocumentRepository documentRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateWorkflowCommand>
{
    public async Task<Result> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
    {
        Document? document = await documentRepository.GetByIdAsync(request.DocumentId, cancellationToken);

        var results = request.Workflows
            .Select(workflow => document.AddWorkflow(
                workflow.ApproverLevel))
            .ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure(ValidationError.FromResults(results));
        }

        foreach (Workflow workflow in document.Workflows)
        {
            workflowRepository.Insert(workflow);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
