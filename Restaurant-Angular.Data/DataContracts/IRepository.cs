using System;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurant_Angular.Data.DataContracts
{
    public interface IRepository<T> where T : class, new()
    {
        //1. Yöntem
       // IQueryable<T> Include<T1>(Expression<Func<T, T1>> selector);

        //1. Yöntem
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T Get(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null);

        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
