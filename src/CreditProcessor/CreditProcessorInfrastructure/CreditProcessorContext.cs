using CreditProcessor.Domain.Entities;
using CreditProcessor.Infrastructure.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CreditProcessor.Infrastructure
{
    public class CreditProcessorContext : DbContext
    {
        public CreditProcessorContext(DbContextOptions options) : base(options) { }

        public 
            DbSet<Credit> Credits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CreditConfiguration());
            modelBuilder.ApplyConfiguration(new InstallmentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
