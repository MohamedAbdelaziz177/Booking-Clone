using BookingClone.Application.Common;
using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class GenericRepo<T> : IGenericRepo<T> where T : class
{

    private readonly DbSet<T> dbSet;
    protected readonly AppDbContext con;

    public GenericRepo(AppDbContext con)
    {
        this.con = con;
        dbSet = con.Set<T>();
    }

    public async Task AddAsync(T item)
    {
        await dbSet.AddAsync(item);
        await con.SaveChangesAsync();
    }

    public async Task DeleteAsync(T item)
    {
        dbSet.Remove(item);
        await con.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        T? entity = await dbSet.FindAsync(id);

        await DeleteAsync(entity);

    }

    public async Task<List<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<List<T>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        )
    {
        var lst = await dbSet.Skip((pageIdx - 1) * pageSize).Take(pageSize).ToListAsync();
        return lst;
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T item)
    {
        dbSet.Update(item);
        await con.SaveChangesAsync();
    }

    public async Task<List<T>> Filter(Expression<Func<T, bool>> predicate,
        string includes = "",
        int PageIdx = 1,
        int PageSize = 8
        )
    {
        IQueryable<T> query = dbSet.Where(predicate);
        query = query.Skip(PageSize * (PageIdx - 1)).Take(PageSize);

        if(includes != string.Empty)
        {
            string[] includeArr = includes.Split(',');

            foreach(string inc in includeArr)
                query.Include(inc);
        }
            

        return await query.ToListAsync();
    }
}
