using System;
using System.Collections.Generic;

namespace SmallRetail.Services
{
    public interface IService<TEntity>
    {
        public IEnumerable<TEntity> GetAll(int limit = 10, int page = 1);
        public TEntity Get(params object[] keyValues);
        public int Count { get; }
        public TEntity Find(Func<TEntity, bool> predicate);
        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);
        public void Create(TEntity entity);
        public void Update(TEntity entity, params object[] keyValues);
        public void Delete(params object[] keyValues);
    }
}