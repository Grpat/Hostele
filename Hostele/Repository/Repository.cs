using System.Linq.Expressions;
using Hostele.Data;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Repository;

public class Repository<T>: IRepository<T> where T:class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    public Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter,string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;
        if (includeProperties != null)
        {
            foreach (var includeProp in
                     includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        query=query.Where(filter);
        return query.FirstOrDefaultAsync();

    }

    public async Task <IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,string? includeProperties=null)
    {
        IQueryable<T> query = dbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        if (includeProperties != null)
        {
            foreach (var includeProp in
                     includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }
    public async Task<List<TType>>GetSelected<TType>(Expression<Func<T, TType>> select,Expression<Func<T, bool>> filter = null)
    {
        
        return await dbSet.Where(filter).Select(select).ToListAsync();
        
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
    public bool CheckIfExists(Expression<Func<T, bool>> expr)
    {
        IQueryable<T> query = dbSet;
        query=query.Where(expr);
        return query.Any();
        
    }
}