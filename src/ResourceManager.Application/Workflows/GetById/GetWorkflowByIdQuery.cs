using ResourceManager.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Application.Workflows.GetById;

public sealed record GetWorkflowByIdQuery(Guid DocumentId) : IQuery<List<WorkflowResponse>>;
