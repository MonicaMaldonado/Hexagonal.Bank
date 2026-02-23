using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Configurations.Context
{
    public class BankDbContext(DbContextOptions<BankDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CreditProduct> CreditProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("bank");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);
        }

    }
}
