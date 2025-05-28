using LeadManager.Application.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace LeadManager.Application.Handlers
{
    public class GetAllLeadsHandler : IRequestHandler<GetAllLeadsQuery, IEnumerable<Lead>>
    {
        private readonly ILeadRepository _repository;
        private readonly ILogger<GetAllLeadsHandler> _logger;

        public GetAllLeadsHandler(ILeadRepository repository, ILogger<GetAllLeadsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Lead>> Handle(GetAllLeadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var leads = await _repository.GetAllAsync();
                _logger.LogInformation("Leads recuperados com sucesso: {Count}", leads is ICollection<Lead> col ? col.Count : -1);
                return leads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao recuperar todos os leads.");
                throw new ApplicationException("Não foi possível recuperar os leads no momento.");
            }
        }
    }
}
