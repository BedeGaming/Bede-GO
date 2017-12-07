using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Bede.Go.Contracts.Interfaces;

namespace Bede.Go.Core.Services
{
    public interface ICrudService<IType> where IType : IIdentifiable
    {
        Task Create(IType entity);
        Task<IType> Read(long id);
        Task<IQueryable<IType>> Read(long[] ids);
        Task Update(IType entity);
        Task Delete(long id);
    }

    public class CrudService<TType> : ICrudService<TType> where TType : class, IIdentifiable
    {
        private readonly Context<TType> _context;

        public CrudService(Context<TType> context)
        {
            _context = context;
        }

        public async Task Create(TType entity)
        {
            _context.DbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task<TType> Read(long id)
        {
            var entity = _context.DbSet.Find(id);
            return entity;
        }

        public async Task<IQueryable<TType>> Read(long[] ids)
        {
            var entities = _context.DbSet.Where(e => ids.Contains(e.Id));
            return entities;
        }

        public async Task Update(TType entity)
        {
            _context.DbSet.AddOrUpdate(entity);
            _context.SaveChanges();
        }

        public async Task Delete(long id)
        {
            var entity= await Read(id).ConfigureAwait(false);
            _context.DbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}