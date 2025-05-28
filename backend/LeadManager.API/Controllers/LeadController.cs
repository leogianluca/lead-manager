using AutoMapper;
using LeadManager.Application.Commands;
using LeadManager.Application.DTOs;
using LeadManager.Application.Queries;
using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LeadManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LeadController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLead([FromBody] CreateLeadCommand command)
        {
            var leadId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLeadById), new { id = leadId }, null);
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> AcceptLead(Guid id)
        {
            await _mediator.Send(new AcceptLeadCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> RejectLead(Guid id)
        {
            await _mediator.Send(new RejectLeadCommand(id));
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeadById(Guid id)
        {
            var lead = await _mediator.Send(new GetLeadByIdQuery(id));
            var leadDto = _mapper.Map<LeadDto>(lead);

            return Ok(leadDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeads()
        {
            var leads = await _mediator.Send(new GetAllLeadsQuery());
            var leadDtos = _mapper.Map<IEnumerable<LeadDto>>(leads);

            return Ok(leadDtos);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetLeadsByStatus(string status)
        {
            if (!Enum.TryParse<LeadStatusEnum>(status, true, out var leadStatus))
                return BadRequest("Status inválido");

            var leads = await _mediator.Send(new GetLeadsByStatusQuery(leadStatus));
            var leadDtos = _mapper.Map<IEnumerable<LeadDto>>(leads);
            
            return Ok(leadDtos);
        }
    }
}
