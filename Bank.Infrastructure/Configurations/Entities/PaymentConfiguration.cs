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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        void IEntityTypeConfiguration<Payment>.Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payment", schema:"bank");

            builder.HasIndex(p => new { p.LoanId, p.Number }).IsUnique();

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnName("numbwer")
                .HasColumnType("int");

            builder.Property(p => p.DueDate)
                .HasColumnName("due_date")
                .IsRequired();

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

            builder.OwnsOne(p => p.Interest, interest =>
            {
                interest.Property(a => a.Amount)        
               .HasColumnName("interest_amount")
               .HasColumnType("decimal(18,2)")
               .IsRequired();

                interest.Property(a => a.Currency)
                .HasColumnName("interest_currency")
                .HasMaxLength(3)
                .IsRequired();
            });

            builder.OwnsOne(p => p.PaidAmount, amount =>
            {
                amount.Property(a => a.Amount)
               .HasColumnName("paid_amount")
               .HasColumnType("decimal(18,2)")
               .IsRequired();

                amount.Property(a => a.Currency)
                .HasColumnName("paid_currency")
                .HasMaxLength(3)
                .IsRequired();
            });


            builder.Property(p => p.Status)
             .IsRequired()
             .HasColumnName("status")
             .HasConversion<string>() //que me lo transforme a string por lo que es un enum
             .HasMaxLength(20);

            builder.Property(p => p.PaymentDate)
             .IsRequired(false)
             .HasColumnName("payment_date");


            builder.HasOne(p => p.Loan)
                .WithMany(p => p.Payments)
                .HasForeignKey(p => p.LoanId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasIndex(p => p.DueDate);
            builder.HasIndex(p => p.Status);

            builder.Ignore(p => p.Total);
        }
    }
}
