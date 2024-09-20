using ResourceManager.Application.Abstractions.Messaging;
using ResourceManager.Domain.Documents;
using ResourceManager.SharedKernel;

namespace ResourceManager.Application.Workflows.GetById;

internal sealed class GetWorkflowByIdQueryHandler(
    IDocumentRepository documentRepository)
    : IQueryHandler<GetWorkflowByIdQuery, List<WorkflowResponse>>
{
    public async Task<Result<List<WorkflowResponse>>> Handle(GetWorkflowByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await documentRepository.GetWorkflowsAsync(request.DocumentId);

        var result = document.Workflows.Select(workflow => new  WorkflowResponse(
            workflow.Id,
            workflow.ApproverLevel,
            workflow.IsCurrentWorkflow
        )).ToList();

        return result;
    }
}
