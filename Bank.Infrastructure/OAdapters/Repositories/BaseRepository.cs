using Bank.Domain.Entities;
using Bank.Domain.Ports.Repositories;
using Bank.Infrastructure.Configurations.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.OAdapters.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly BankDbContext _context;

    public BaseRepository(BankDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        var response = await _context.Set<TEntity>().AddAsync(entity);
        return response.Entity;
    }

    public TEntity UpdateAsync(TEntity entity)
    {
        var response = _context.Set<TEntity>().Update(entity);
        return response.Entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<ICollection<TEntity>> ListAsync()
    {
        return await _context.Set<TEntity>()
            .Where(x => x.IsActive)
            .AsNoTracking()
            .ToListAsync();

    }

    public async Task<(ICollection<TResult> Collection, int totalCount)> ListAsync<TResult>
    (
        Expression<Func<TEntity, bool>> predicate, //este es el predicado, que no es mas que los elementos que se van a pasar en el where de la consulta, y este devuelve un true or false
        Expression<Func<TEntity, TResult>> selector, //el selector es el resultado serializado que le estamos indicando
        int pageNumber=1, //propiedades por defecto que van para la paginadcion
        int pageSize= 10
    )
    {
        var response = await _context.Set<TEntity>()
            .Where(predicate)
            .Skip((pageNumber - 1) * pageSize) //cantidad de paginas hacia atras multiplicado por el tamaño de pagina, 
            .Take(pageSize) //cantidad de elementos a tomar
            .Select(selector) //seleccionamos el resultado serializado
            .ToListAsync();

        var total = await _context.Set<TEntity>()
            .Where(predicate)
            .CountAsync();

        return (response, total);
        /*
        return await _context.Set<TEntity>()
            .Where(predicate)
            .Where(x => x.IsActive)
            .AsNoTracking()
            .Select(selector)
            .PaginateAsync(pageNumber, pageSize);*/
    }
}

