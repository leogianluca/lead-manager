using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using MediatR;
using System.Collections.Generic;

namespace LeadManager.Application.Queries
{
    public record GetLeadsByStatusQuery(LeadStatusEnum Status) : IRequest<IEnumerable<Lead>>;
}
