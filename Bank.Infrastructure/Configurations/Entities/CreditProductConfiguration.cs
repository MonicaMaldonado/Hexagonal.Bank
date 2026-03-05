using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Configurations.Entities
{
    public class CreditProductConfiguration : IEntityTypeConfiguration<CreditProduct>
    {
        public void Configure(EntityTypeBuilder<CreditProduct> builder)
        {
            builder.ToTable("credit_product", schema:"bank");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.OwnsOne(p => p.MinimumAmount, amount =>
            {
                amount.Property(a => a.Amount)
                    .HasColumnName("minimum_amount_value")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                amount.Property(a => a.Currency)
                    .HasColumnName("minimum_amount_currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            builder.OwnsOne(p => p.MaximumAmount, amount => 
            {
                amount.Property(a => a.Amount)
                .HasColumnName("maximum_amount_value")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

                amount.Property(a => a.Currency)
                .HasColumnName("maximum_amount_currency")
                .HasMaxLength(3)
                .IsRequired();
            });

            builder.OwnsOne(p => p.AnnualInterestRate, rate =>
            {
                rate.Property(r => r.Value)
                    .HasColumnName("annual_interest_rate")
                    .HasColumnType("decimal(5,2)")
                    .IsRequired();
            });

            builder.Property(p => p.MinimumTerm).IsRequired();
            
            builder.Property(p => p.MaximumTerm).IsRequired();
        }
    }
}
