using LeadManager.Domain.Entities;
using LeadManager.Domain.Enums;
using LeadManager.Domain.Interfaces;
using LeadManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManager.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;

        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Lead> GetByIdAsync(Guid id)
        {
            return await _context.Leads.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Lead>> GetAllAsync()
        {
            return await _context.Leads.ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatusEnum status)
        {
            return await _context.Leads
                .Where(l => l.Status == status)
                .ToListAsync();
        }

        public async Task AddAsync(Lead lead)
        {
            try
            {
                await _context.Leads.AddAsync(lead);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Erro ao salvar no banco:");
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("InnerException: " + ex.InnerException?.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }

        }

        public async Task UpdateAsync(Lead lead)
        {
            _context.Leads.Update(lead);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var lead = await GetByIdAsync(id);
            if (lead != null)
            {
                _context.Leads.Remove(lead);
                await _context.SaveChangesAsync();
            }
        }
    }
}
