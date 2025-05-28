using System;
using LeadManager.Domain.Entities;

namespace LeadManager.Domain.Events
{
    public class LeadAcceptedEvent
    {
        public Guid LeadId { get; }
        public string EmailToNotify { get; } = "vendas@test.com";
        public LeadAcceptedEvent(Guid leadId)
        {
            LeadId = leadId;
        }
    }
}
