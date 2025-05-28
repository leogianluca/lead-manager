using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManager.Domain.Interfaces
{
    public interface ILeadRepository
    {
        Task<Lead> GetByIdAsync(Guid id);
        Task<IEnumerable<Lead>> GetAllAsync();
        Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatusEnum status);
        Task AddAsync(Lead lead);
        Task UpdateAsync(Lead lead);
        Task DeleteAsync(Guid id);
    }
}
