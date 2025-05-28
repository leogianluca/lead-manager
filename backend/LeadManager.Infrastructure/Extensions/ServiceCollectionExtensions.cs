using LeadManager.Domain.Interfaces;
using LeadManager.Infrastructure.Data;
using LeadManager.Infrastructure.Repositories;
using LeadManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeadManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }
    }
}
