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
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        void IEntityTypeConfiguration<Loan>.Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loan", schema:"bank");

            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.CustomerId);

            builder.OwnsOne(p => p.Amount, amount =>
            {
                amount.Property(a => a.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

                amount.Property(a => a.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();
            });

            builder.OwnsOne(p => p.InterestRate, rate =>
            {
                rate.Property(a => a.Value)
                .HasColumnName("interest_rate")
                .HasColumnType("decimal(5,2)")
                .IsRequired();
            });

            builder.OwnsOne(p => p.Term, term =>
            {
                term.Property(a => a.Months)
                .HasColumnName("term_month")
                .HasColumnType("int")
                .IsRequired();
            });

            builder.Property(p => p.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<string>() //que me lo transforme a string por lo que es un enum
                .HasMaxLength(20);

            builder.Property(p => p.RejectionReason)
                .IsRequired(false)
                .HasColumnName("rejection_reason")
                .HasMaxLength(500);

            builder.HasOne(p => p.Customer)
                .WithMany(p => p.Loans)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(p => p.Payments)
                .WithOne(p => p.Loan)
                .HasForeignKey(p => p.LoanId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Loan.Payments))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(p => p.TotalInterest);
            builder.Ignore(p => p.TotalAmount);
        }
    }
}
