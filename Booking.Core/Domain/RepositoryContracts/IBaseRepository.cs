using Booking.Core.Domain.Entities;
using Booking.Core.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Core.Domain.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<TResult>> FindAll<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> projection, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int skip, int take);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, OrderBy order=OrderBy.Ascending);

        Task<T> Add(T entity);

        T Update(T entity);

        void Delete(T entity);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
