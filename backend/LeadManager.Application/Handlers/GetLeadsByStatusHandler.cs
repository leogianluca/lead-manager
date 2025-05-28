using LeadManager.Application.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using LeadManager.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LeadManager.Application.Handlers
{
    public class GetLeadsByStatusHandler : IRequestHandler<GetLeadsByStatusQuery, IEnumerable<Lead>>
    {
        private readonly ILeadRepository _repository;
        private readonly ILogger<GetLeadsByStatusHandler> _logger;

        public GetLeadsByStatusHandler(ILeadRepository repository, ILogger<GetLeadsByStatusHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Lead>> Handle(GetLeadsByStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var leads = await _repository.GetByStatusAsync(request.Status);
                if (leads == null)
                {
                    _logger.LogWarning("Nenhum lead encontrado com status {Status}", request.Status);
                    return new List<Lead>();
                }
                return leads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter leads com status {Status}", request.Status);
                throw new Exception("Erro ao buscar leads por status");
            }
        }
    }
}
