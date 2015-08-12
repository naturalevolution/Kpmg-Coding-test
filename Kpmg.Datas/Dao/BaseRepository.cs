using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Kpmg.Datas.Contexts;
using Kpmg.Datas.Models;

namespace Kpmg.Datas.Dao
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected IKpmgContext CurrentContext;

        protected BaseRepository(IKpmgContext context)
        {
            CurrentContext = context;
        }

        public void Insert(T entity)
        {
            GetDbSet().Add(entity);
        }

        public T FindBy(Func<T, bool> query)
        {
            return GetDbSet().Find(query);
        }

        public List<T> FindAll()
        {
            return GetDbSet().OrderBy(x => x.Id).ToList();
        }

        protected abstract IDbSet<T> GetDbSet();
    }
}