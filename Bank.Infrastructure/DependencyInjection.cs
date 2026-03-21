using Bank.Domain.Ports.Repositories;
using Bank.Domain.Ports.Services;
using Bank.Infrastructure.Configurations.Context;
using Bank.Infrastructure.OAdapters.Repositories;
using Bank.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BankDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("BdBanking"));
            });

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<ICreditProductRepository, CreditProductRepository>();

            return service;
        }
    }
}
