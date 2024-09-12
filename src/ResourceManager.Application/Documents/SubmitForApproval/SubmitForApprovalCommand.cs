using ResourceManager.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Application.Documents.SubmitForApproval;

public sealed record SubmitForApprovalCommand(Guid DocumentId, Guid UserId) :ICommand;
