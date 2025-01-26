﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Entities;
using Infrastructure.Data;
using Core.Interfases;
namespace Infrastructure.Repositories;

public class GenericRepository<T>(CountriesContext context) : IGenericRepository<T> where T : BaseEntity
{
    protected readonly CountriesContext _context = context;

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
    int pageIndex, int pageSize, string search)
    {
        var totalRegistros = await _context.Set<T>()
                            .CountAsync();

        var registros = await _context.Set<T>()
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return (totalRegistros, registros);

    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>()
            .Update(entity);
    }
}

