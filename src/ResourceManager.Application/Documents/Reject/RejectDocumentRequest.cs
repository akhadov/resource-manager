﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Application.Documents.Reject;

public sealed record RejectDocumentRequest(string Reason);
