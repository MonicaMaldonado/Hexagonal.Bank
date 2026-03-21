using Bank.Domain.Ports.Repositories;
using Bank.Domain.Ports.Services;
using Bank.Infrastructure.Configurations.Context;
using Bank.Infrastructure.OAdapters.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly BankDbContext _context;
    private IDbContextTransaction? _currentTransaction;
    private ICreditProductRepository _creditProductRepository { get;  }

    public UnitOfWork(BankDbContext context)
    {
        _context = context;
        _creditProductRepository = new CreditProductRepository(_context);
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransaction()
    {
        if (_currentTransaction != null)
            throw new InvalidOperationException("A transaction is already in progress");

        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No transaction in progress to commit");
        try
        {
            await _context.SaveChangesAsync();
            await _currentTransaction.CommitAsync();
        }
        catch
        {
            await RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
    
    public async Task RollbackTransaction()
    {
        if (_currentTransaction == null)
            throw new InvalidOperationException("No transaction in progress to rollback");
        try
        {
            await _currentTransaction.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
        await _context.DisposeAsync();
    }
}