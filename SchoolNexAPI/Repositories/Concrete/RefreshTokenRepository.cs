using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.Models;
using SchoolNexAPI.Repositories.Abstract;

namespace SchoolNexAPI.Repositories.Concrete
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshTokenModel refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshTokenModel> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task DeleteTokensByUserIdAsync(string userId)
        {
            var tokens = _context.RefreshTokens.Where(r => r.UserId == userId);
            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshTokenModel refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task CleanupExpiredAsync()
        {
            var tokensToDelete = await _context.RefreshTokens
                .Where(x => x.Expires < DateTime.UtcNow || x.IsRevoked || x.IsUsed)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(tokensToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountActiveTokensAsync(string userId)
        {
            return await _context.RefreshTokens
                .CountAsync(x => x.UserId == userId && !x.IsRevoked && !x.IsUsed && x.Expires > DateTime.UtcNow);
        }

        public async Task RevokeOldestTokenAsync(string userId)
        {
            var oldest = await _context.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked && !x.IsUsed && x.Expires > DateTime.UtcNow)
                .OrderBy(x => x.Created)
                .FirstOrDefaultAsync();

            if (oldest != null)
            {
                oldest.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
