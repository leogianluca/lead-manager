using LeadManager.Application.Commands;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using LeadManager.Domain.Interfaces;
using LeadManager.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LeadManager.Application.Handlers
{
    public class CreateLeadHandler : IRequestHandler<CreateLeadCommand, Guid>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly ILogger<CreateLeadHandler> _logger;

        public CreateLeadHandler(ILeadRepository leadRepository, ILogger<CreateLeadHandler> logger)
        {
            _leadRepository = leadRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
        {

            var categoryEnum = Enum.TryParse(request.Category, true, out CategoryEnum cat) 
                                ? cat 
                                : throw new ArgumentException("Categoria inválida");

            var lead = new Lead(
                new PersonName(request.FirstName),
                new PersonName(request.LastName),
                new Email(request.Email),
                new Phone(request.Phone),
                new Suburb(request.Suburb),
                categoryEnum,
                request.Description,
                new Price(request.Price)
            );

            await _leadRepository.AddAsync(lead);

            _logger.LogInformation("Lead criado com ID {LeadId}", lead.Id);

            return lead.Id;
        }
    }
}
