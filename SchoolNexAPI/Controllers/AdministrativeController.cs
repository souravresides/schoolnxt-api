using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs.Administrative;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Concrete;
using System.Security.Claims;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrativeController : BaseController
    {
        private readonly IAdministrativeService _administrativeService;
        private readonly UserManager<AppUserModel> _userManager;

        public AdministrativeController(IAdministrativeService administrativeService, UserManager<AppUserModel> userManager)
        {
            this._administrativeService = administrativeService;
            this._userManager = userManager;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var schoolId = GetSchoolId();
            var response = await _administrativeService.GetUsersAsync(schoolId);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response.Users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await _administrativeService.GetUserByIdAsync(id);
            if (!response.IsSuccess)
                return NotFound(response);
            return Ok(response.Users.FirstOrDefault());
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            var response = await _administrativeService.UpdateUserAsync(id, model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(new { Message = "User updated successfully." });
        }

        [HttpPut("users/{id}/roles")] // plural to reflect multiple roles
        public async Task<IActionResult> ChangeUserRoles(string id, [FromBody] List<string> roles)
        {
            var response = await _administrativeService.ChangeUserRolesAsync(id, roles);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(new { Message = "User roles updated successfully." });
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _administrativeService.GetAllRolesAsync();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response.Roles);
        }

        [HttpGet("roles/{roleName}/users")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var schoolId = GetSchoolId();
            var response = await _administrativeService.GetUsersByRoleAsync(roleName, schoolId);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response.Users);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = _userManager.GetUserId(User);
            var response = await _administrativeService.GetUserProfileAsync(userId);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto model)
        {
            var userId = _userManager.GetUserId(User);
            var response = await _administrativeService.UpdateUserProfileAsync(userId, model);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("UploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(Guid userId, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("No file uploaded");

            var url = await _administrativeService.UpdateProfilePictureAsync(userId.ToString(), photo);
            return Ok(new { url });
        }


    }
}
