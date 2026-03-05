using Bank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Ports.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> AddAsync(TEntity entity);
        TEntity UpdateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<ICollection<TEntity>> ListAsync();
        Task<(ICollection<TResult> Collection, int totalCount)> ListAsync<TResult>
        (
            Expression<Func<TEntity, bool>> predicate,
            System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector,
            int pageNumber = 1,
            int pageSize = 10
        );
    }
}
