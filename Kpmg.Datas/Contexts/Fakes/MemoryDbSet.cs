using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Kpmg.Datas.Models;

namespace Kpmg.Datas.Contexts.Fakes
{
    public class MemoryDbSet<T> : IDbSet<T>
        where T : BaseEntity
    {
        private readonly IQueryable _query;

        public MemoryDbSet()
        {
            Local = new ObservableCollection<T>();
            _query = Local.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            return Local.FirstOrDefault((Func<T, bool>) keyValues.Single());
        }

        public T Add(T item)
        {
            if (item.Id > 0)
            {
                throw new InvalidDataException(string.Format("Cannot insert an existing entity id : {0}", item.Id));
            }
            item.Id = Local.Count + 1;
            Local.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            Local.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            Local.Add(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local { get; private set; }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Local.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return Local.GetEnumerator();
        }

        public T Detach(T item)
        {
            Local.Remove(item);
            return item;
        }
    }
}