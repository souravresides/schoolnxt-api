using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs.Employee;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, Guid schoolId, string createdBy)
        {
            var employee = new EmployeeModel
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                JoiningDate = dto.JoiningDate,
                SchoolId = schoolId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy,
                IsActive = true
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> UpdateAsync(Guid id, UpdateEmployeeDto dto, Guid schoolId, string updatedBy)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.SchoolId == schoolId && e.IsActive);
            if (employee == null) return null;

            employee.FullName = dto.FullName;
            employee.Email = dto.Email;
            employee.ContactNumber = dto.ContactNumber;
            employee.JoiningDate = dto.JoiningDate;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.UpdatedBy = updatedBy;

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid id, Guid schoolId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.SchoolId == schoolId && e.IsActive);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<List<EmployeeDto>> GetAllAsync(Guid schoolId)
        {
            var employees = await _context.Employees
                .Where(e => e.SchoolId == schoolId && e.IsActive)
                .ToListAsync();

            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid schoolId, string deletedBy)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.SchoolId == schoolId && e.IsActive);
            if (employee == null) return false;

            employee.IsActive = false;
            employee.UpdatedAt = DateTime.UtcNow;
            employee.UpdatedBy = deletedBy;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
