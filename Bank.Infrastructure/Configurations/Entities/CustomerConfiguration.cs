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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer", schema:"bank");
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("last_name");


            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("email");

            builder.OwnsOne(p => p.Document, document =>
            {
                document.Property(d => d.Number)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("document_number");

                document.Property(d => d.Type)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("document_type");

                document.HasIndex(d => new { d.Type, d.Number }).IsUnique(); //llave unica con los campos

            });

            builder.Property(p => p.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<string>() //que me lo transforme a string por lo que es un enum
                .HasMaxLength(20);

            builder.HasMany(p => p.Loans)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Metadata
                .FindNavigation(nameof(Customer.Loans))?
                .SetPropertyAccessMode(PropertyAccessMode.Field); //establece por cual columna hacemos la relacion directa
        }
    }
}
