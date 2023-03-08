using CreditProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditProcessor.Infrastructure.EntitiesConfigurations
{
    public class InstallmentConfiguration : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.ToTable("Installments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.CreditId)
                .IsRequired();

            builder.Property(x => x.Number)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.UpdatedOn);

            builder.Property(x => x.DueDate)
                .IsRequired();

            builder.Property(x => x.TaxAmount)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.AmountWithTax)
                .IsRequired();

            builder.HasOne(x => x.Credit);
                
        }
    }
}
