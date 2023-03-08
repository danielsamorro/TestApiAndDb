using CreditProcessor.Domain.Entities;
using CreditProcessor.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditProcessor.Infrastructure.EntitiesConfigurations
{
    public class CreditConfiguration : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> builder)
        {
            builder.ToTable("Credits");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.CreditStatus)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (CreditStatus)Enum.Parse(typeof(CreditStatus), v));

            builder.Property(x => x.CreditType)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (CreditType)Enum.Parse(typeof(CreditType), v));

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.UpdatedOn);

            builder.Property(x => x.TaxAmount)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired();

            builder.Property(x => x.TotalAmountWithTax)
                .IsRequired();

            builder.HasMany(x => x.Installments)
                .WithOne(y => y.Credit)
                .HasForeignKey(x => x.CreditId);
        }
    }
}
