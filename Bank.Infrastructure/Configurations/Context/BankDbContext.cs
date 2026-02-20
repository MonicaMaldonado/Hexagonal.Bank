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

    }
}
