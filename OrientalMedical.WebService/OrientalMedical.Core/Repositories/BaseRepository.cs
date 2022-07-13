using Microsoft.EntityFrameworkCore;
using OrientalMedical.Damin.Interfaces;
using OrientalMedical.Damin.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace OrientalMedical.Core.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepositorioBase<TEntity> where TEntity : class
    {
        private readonly OrientalMedicalSystemDBContext _context;
        public BaseRepository(OrientalMedicalSystemDBContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
