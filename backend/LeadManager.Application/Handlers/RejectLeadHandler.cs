using LeadManager.Application.Commands;
using LeadManager.Domain.Enums;
using LeadManager.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LeadManager.Application.Handlers
{
    public class RejectLeadHandler : IRequestHandler<RejectLeadCommand, Unit>
    {
        private readonly ILeadRepository _repository;
        private readonly ILogger<RejectLeadHandler> _logger;

        public RejectLeadHandler(ILeadRepository repository, ILogger<RejectLeadHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RejectLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await _repository.GetByIdAsync(request.LeadId);

                if (lead == null)
                {
                    _logger.LogWarning("Lead com ID {LeadId} não encontrado para rejeição.", request.LeadId);
                    throw new Exception("Lead não encontrado");
                }

                if (lead.Status != LeadStatusEnum.Invited)
                {
                    _logger.LogWarning("Tentativa de rejeitar lead com ID {LeadId} que já está em status {Status}.", request.LeadId, lead.Status);
                    throw new InvalidOperationException("Somente leads com status 'Convidado' podem ser rejeitados");
                }

                lead.Reject();

                await _repository.UpdateAsync(lead);

                _logger.LogInformation("Lead com ID {LeadId} foi rejeitado com sucesso.", request.LeadId);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao rejeitar lead com ID {LeadId}.", request.LeadId);
                throw;
            }
        }
    }
}
