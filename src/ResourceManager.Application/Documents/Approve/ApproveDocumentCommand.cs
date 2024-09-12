using ResourceManager.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Application.Documents.Approve;

public sealed record ApproveDocumentCommand(
    Guid DocumentId,
    Guid ApproverId) : ICommand;
