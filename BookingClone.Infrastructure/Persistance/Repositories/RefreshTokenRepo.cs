
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace BookingClone.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepo : GenericRepo<RefreshToken>, IRefreshRokenRepo
{ 
    public RefreshTokenRepo(AppDbContext con) : base(con)
    {
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await con.refreshTokens.FirstOrDefaultAsync(r => r.Token == token);
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(string userId)
    {
        return await con.refreshTokens.Where(r => r.UserId == userId).ToListAsync();
    }
}
