using SchoolNexAPI.Models;

namespace SchoolNexAPI.Repositories.Abstract
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshTokenModel refreshToken);
        Task<RefreshTokenModel> GetByTokenAsync(string token);
        Task DeleteTokensByUserIdAsync(string userId);
        Task UpdateAsync(RefreshTokenModel refreshToken);
        Task CleanupExpiredAsync();
        Task<int> CountActiveTokensAsync(string userId);
        Task RevokeOldestTokenAsync(string userId);
    }
}
