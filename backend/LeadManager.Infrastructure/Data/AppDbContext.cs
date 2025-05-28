using LeadManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lead> Leads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lead>(lead =>
            {
                //lead.ToTable("Leads", "dbo");  // <-- Explicitamente informar schema 'dbo'

                lead.OwnsOne(l => l.FirstName, fn =>
                {
                    fn.Property(p => p.Value).HasColumnName("FirstName");
                });

                lead.OwnsOne(l => l.LastName, ln =>
                {
                    ln.Property(p => p.Value).HasColumnName("LastName");
                });

                lead.OwnsOne(l => l.Email, e =>
                {
                    e.Property(p => p.Address).HasColumnName("Email");
                });

                lead.OwnsOne(l => l.Phone, p =>
                {
                    p.Property(x => x.Number).HasColumnName("Phone");
                });

                lead.OwnsOne(l => l.Suburb, s =>
                {
                    s.Property(x => x.Value).HasColumnName("Suburb");
                });

                lead.OwnsOne(l => l.Price, price =>
                {
                    price.Property(p => p.Value)
                         .HasColumnName("Price")
                         .HasPrecision(18, 2); // 18 dígitos totais, 2 decimais
                });
            });
        }
    }
}
