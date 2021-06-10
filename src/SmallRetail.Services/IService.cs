using System;
using System.Collections.Generic;

namespace SmallRetail.Services
{
    public interface IService<TEntity>
    {
        public IEnumerable<TEntity> GetAll();
        public TEntity Get(params object[] keyValues);
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(params object[] keyValues);
    }
}