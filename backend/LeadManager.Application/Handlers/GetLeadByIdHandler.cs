using LeadManager.Application.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LeadManager.Application.Handlers
{
    public class GetLeadByIdHandler : IRequestHandler<GetLeadByIdQuery, Lead>
    {
        private readonly ILeadRepository _repository;
        private readonly ILogger<GetLeadByIdHandler> _logger;

        public GetLeadByIdHandler(ILeadRepository repository, ILogger<GetLeadByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Lead> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await _repository.GetByIdAsync(request.LeadId);
                if (lead == null)
                {
                    _logger.LogWarning("Lead com Id {LeadId} não encontrado.", request.LeadId);
                    throw new Exception("Lead não encontrado");
                }
                return lead;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar lead com Id {LeadId}.", request.LeadId);
                throw;
            }
        }
    }
}
