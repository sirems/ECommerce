using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface IRepository<T> where T : class
    {
        //id vererek tek kayıt getirme
        T Get(int id); 
            
        //çoklu kayıt getirme, belirli bir filtreleme ile,tablo birleştirerek vs
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        //çoklu kayıt silme
        void RemoveRange(IEnumerable<T> entity);
    }
}
