using System;
using System.Collections.Generic;
using Kpmg.Datas.Models;

namespace Kpmg.Datas.Dao
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Insert(T entity);
        T FindBy(Func<T, bool> func);
        List<T> FindAll();
    }
}