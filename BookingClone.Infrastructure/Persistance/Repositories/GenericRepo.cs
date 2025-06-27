using BookingClone.Domain.IRepositories;
using BookingClone.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BookingClone.Infrastructure.Persistance.Repositories;
public class GenericRepo<T> : IGenericRepo<T> where T : class
{

    private readonly DbSet<T> dbSet;
    private readonly AppDbContext con;

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

    public async Task<List<T>> GetAllAsync(int pageIdx = 1,
        int pageSize = 8,
        string sortField = "id",
        string sortDir = "desc"
        )
    {
        return new List<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(T item)
    {
        dbSet.Update(item);
        await con.SaveChangesAsync();
    }
}
