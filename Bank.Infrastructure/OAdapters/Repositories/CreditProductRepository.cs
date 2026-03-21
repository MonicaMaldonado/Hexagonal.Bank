using Bank.Domain.Entities;
using Bank.Domain.Ports.Repositories;
using Bank.Infrastructure.Configurations.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.OAdapters.Repositories
{
    public class CreditProductRepository : BaseRepository<CreditProduct>, ICreditProductRepository
    {
        public CreditProductRepository(BankDbContext context) : base(context) { }
    }
}
