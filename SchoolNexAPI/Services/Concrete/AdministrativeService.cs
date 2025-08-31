using Microsoft.AspNetCore.Identity;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Administrative;
using SchoolNexAPI.DTOs.Auth;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class AdministrativeService : IAdministrativeService
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrativeService(UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<AuthResponseDto> GetUsersAsync()
        {
            try
            {
                // Fetch all users from Identity
                var users = _userManager.Users.ToList();

                // Map them to a DTO (never return password info)
                var userList = new List<UserDto>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    userList.Add(new UserDto
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Roles = roles.ToList()
                    });
                }

                return new AuthResponseDto
                {
                    IsSuccess = true,
                    Users = userList
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Error fetching users: {ex.Message}" }
                };
            }
        }

        public async Task<AuthResponseDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = roles.ToList()
            };

            return new AuthResponseDto
            {
                IsSuccess = true,
                Users = new List<UserDto> { userDto }
            };
        }


        public async Task<AuthResponseDto> UpdateUserAsync(string userId, UpdateUserDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            // Update properties
            user.Name = model.Name ?? user.Name;
            user.Email = model.Email ?? user.Email;
            user.UserName = model.Email ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponseDto { IsSuccess = true };
        }

        public async Task<AuthResponseDto> ChangeUserRolesAsync(string userId, List<string> newRoleIds)
        {
            if (newRoleIds == null)
                newRoleIds = new List<string>();

            // Find the user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            // Get current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove old roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = removeResult.Errors.Select(e => e.Description).ToList()
                };
            }

            // Map role IDs to role names
            var rolesToAdd = new List<string>();
            foreach (var roleId in newRoleIds)
            {
                //var role = await _roleManager.FindByIdAsync(roleId);
                if (roleId != null)
                    rolesToAdd.Add(roleId);
            }

            // Add new roles
            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    // Rollback: re-add old roles if add fails
                    if (currentRoles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, currentRoles);
                    }

                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Errors = addResult.Errors.Select(e => e.Description).ToList()
                    };
                }
            }

            return new AuthResponseDto
            {
                IsSuccess = true,
                //Message = $"Roles updated. Removed: {string.Join(", ", currentRoles)}, Added: {string.Join(", ", rolesToAdd)}"
            };
        }



        public async Task<AuthResponseDto> GetAllRolesAsync()
        {
            try
            {
                var roles = _roleManager.Roles
                    .Select(r => new RoleDto
                    {
                        Id = r.Id,
                        Name = r.Name
                    })
                    .ToList();

                return new AuthResponseDto
                {
                    IsSuccess = true,
                    Roles = roles
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Error fetching roles: {ex.Message}" }
                };
            }
        }

        public async Task<AuthResponseDto> GetUsersByRoleAsync(string roleName)
        {
            try
            {
                var users = await _userManager.GetUsersInRoleAsync(roleName);

                var userList = users.Select(u => new UserDto
                {
                    UserId = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Roles = new List<string> { roleName }
                }).ToList();

                return new AuthResponseDto
                {
                    IsSuccess = true,
                    Users = userList
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { $"Error fetching users for role {roleName}: {ex.Message}" }
                };
            }
        }

        public async Task<AuthResponseDto> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var userDto = new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            return new AuthResponseDto
            {
                IsSuccess = true,
                User = userDto
            };
        }

        public async Task<AuthResponseDto> UpdateUserProfileAsync(string userId, UpdateUserProfileDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            var updatedUserDto = new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                ProfilePicture = user.ProfilePicture,
                PhoneNumber = user.PhoneNumber
            };

            return new AuthResponseDto
            {
                IsSuccess = true,
                User = updatedUserDto
            };
        }

    }
}
