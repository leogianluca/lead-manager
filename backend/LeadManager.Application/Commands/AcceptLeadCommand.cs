using MediatR;
using System;

namespace LeadManager.Application.Commands
{
    public class AcceptLeadCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public AcceptLeadCommand(Guid id)
        {
            Id = id;
        }
    }
}
