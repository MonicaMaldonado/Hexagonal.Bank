using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Configurations.Context
{
    public class BankDbContextFactory : IDesignTimeDbContextFactory<BankDbContext>
    {
        public BankDbContext CreateDbContext(string[] args)
        {
            var devConnectionString = "Host=localhost;Port=1502;Database=bd_banking;Username=admin;Password=123456";
            var connectionString = Environment.GetEnvironmentVariable("dbBanking") ?? devConnectionString;

            var optionBuilder = new DbContextOptionsBuilder<BankDbContext>(); //genera una instancia del contexto de la bd

            optionBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsHistoryTable("__EFMigrationHistory","bank");
            });

            return new BankDbContext(optionBuilder.Options);

        }
    }
}
