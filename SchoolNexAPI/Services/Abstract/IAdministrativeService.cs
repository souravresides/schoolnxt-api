using SchoolNexAPI.DTOs.Administrative;
using SchoolNexAPI.DTOs.Auth;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IAdministrativeService
    {
        Task<AuthResponseDto> GetUsersAsync(Guid schoolId);
        Task<AuthResponseDto> GetUserByIdAsync(string userId);
        Task<AuthResponseDto> UpdateUserAsync(string userId, UpdateUserDto model);
        Task<AuthResponseDto> ChangeUserRolesAsync(string userId, List<string> newRoles);
        Task<AuthResponseDto> GetUsersByRoleAsync(string roleName, Guid schoolId);
        Task<AuthResponseDto> GetAllRolesAsync();
        Task<AuthResponseDto> GetUserProfileAsync(string userId);
        Task<AuthResponseDto> UpdateUserProfileAsync(string userId, UpdateUserProfileDto model);
        Task<string> UpdateProfilePictureAsync(string userId, IFormFile file);
    }
}
