using LeadManager.Application.Commands;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Exceptions;
using LeadManager.Domain.Interfaces;
using LeadManager.Infrastructure.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LeadManager.Application.Handlers
{
    public class AcceptLeadHandler : IRequestHandler<AcceptLeadCommand, Unit>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IEmailSender _emailSender;

        public AcceptLeadHandler(ILeadRepository leadRepository, IEmailSender emailSender)
        {
            _leadRepository = leadRepository;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(AcceptLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _leadRepository.GetByIdAsync(request.Id);
            if (lead == null)
                throw new NotFoundException("Lead não encontrado.");

            if (lead.Status != Domain.Enums.LeadStatusEnum.Invited)
                throw new ValidationException("Apenas leads com status 'Invited' podem ser aceitos.");

            lead.Accept();

            await _leadRepository.UpdateAsync(lead);

            await _emailSender.SendEmailAsync(
               lead.Email.ToString(),
               "Seu lead foi aceito!",
               $"Olá {lead.FirstName}, seu lead foi aceito com sucesso!"
           );

            return Unit.Value;
        }
    }
}
