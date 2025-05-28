using LeadManager.Domain.Entities;
using MediatR;
using System;

namespace LeadManager.Application.Queries
{
    public record GetLeadByIdQuery(Guid LeadId) : IRequest<Lead>;
}
