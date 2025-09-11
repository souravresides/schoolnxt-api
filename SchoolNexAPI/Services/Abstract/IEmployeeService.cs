using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Employee;
using SchoolNexAPI.Migrations;
using SchoolNexAPI.Models;

namespace SchoolNexAPI.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<EmployeeModel> CreateStaffAsync(EmployeeDto staffDto,Guid? SchoolId);
        Task<EmployeeModel?> GetStaffByIdAsync(Guid id, Guid? SchoolId);
        Task<List<EmployeeModel>> GetAllStaffAsync(Guid? SchoolId);
        Task<EmployeeModel?> UpdateStaffAsync(Guid id, EmployeeDto staffDto, Guid? SchoolId);
        Task<bool> DeleteStaffAsync(Guid id, Guid? SchoolId);
    }

}
