using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Ports.Services
{
    public interface IUnitOfWork
    {
        Task RollbackTransaction();
        Task CommitTransaction();
        Task BeginTransaction();
        Task<int> SaveChangeAsync();
    }
}
