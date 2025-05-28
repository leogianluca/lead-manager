using MediatR;
using System;

namespace LeadManager.Application.Commands
{
    public record RejectLeadCommand(Guid LeadId) : IRequest<Unit>;
}
