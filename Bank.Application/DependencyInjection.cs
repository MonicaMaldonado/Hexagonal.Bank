using Bank.Application.Ports.CredictProduct;
using Bank.Application.UseCases.CreditProductUseCase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<ICreateCredictProductUseCase, CreateCreditProductUseCase>();

            return service;
        }
    }
}
