using System.Linq.Expressions;

namespace Hostele.Repository;

public interface IRepository<T> where T:class
{
    Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter,string? includeProperties=null);
    Task <IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,string? includeProperties=null);
    Task<List<TType>> GetSelected<TType>(Expression<Func<T, TType>> select,Expression<Func<T, bool>> filter = null);
    void Add(T entity);
    void Remove(T entity);
    bool CheckIfExists(Expression<Func<T, bool>> expr);

}