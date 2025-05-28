using LeadManager.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace LeadManager.Application.Queries
{
    public record GetAllLeadsQuery() : IRequest<IEnumerable<Lead>>;
}
