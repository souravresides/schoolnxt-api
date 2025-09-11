using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.DTOs.Employee;
using SchoolNexAPI.Models;
using SchoolNexAPI.Services.Abstract;

namespace SchoolNexAPI.Services.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeModel> CreateStaffAsync(EmployeeDto staffDto, Guid? SchoolId)
        {
            var staff = new EmployeeModel
            {
                EmployeeId = GenerateEmployeeId(),
                SchoolId = SchoolId ?? Guid.Empty,
                MobileNumber = staffDto.MobileNumber,
                Email = staffDto.Email,
                FirstName = staffDto.FirstName,
                MiddleName = staffDto.MiddleName,
                LastName = staffDto.LastName,
                DateOfBirth = staffDto.DateOfBirth,
                Gender = staffDto.Gender,
                BloodGroup = staffDto.BloodGroup,
                UserRole = staffDto.UserRole,
                Department = staffDto.Department,
                CurrentAddressLine1 = staffDto.CurrentAddressLine1,
                CurrentAddressLine2 = staffDto.CurrentAddressLine2,
                CurrentCity = staffDto.CurrentCity,
                CurrentState = staffDto.CurrentState,
                CurrentPinCode = staffDto.CurrentPinCode,
                CurrentCountry = staffDto.CurrentCountry,
                PermanentAddressLine1 = staffDto.PermanentAddressLine1,
                PermanentAddressLine2 = staffDto.PermanentAddressLine2,
                PermanentCity = staffDto.PermanentCity,
                PermanentState = staffDto.PermanentState,
                PermanentPinCode = staffDto.PermanentPinCode,
                PermanentCountry = staffDto.PermanentCountry,
                JobTitle = staffDto.JobTitle,
                Designation = staffDto.Designation,
                EmploymentType = staffDto.EmploymentType,
                AppointmentDate = staffDto.AppointmentDate,
                ExperienceYears = staffDto.ExperienceYears,
                HighestQualification = staffDto.HighestQualification,
                UAN = staffDto.UAN,
                PFAccountNumber = staffDto.PFAccountNumber,
                ESICodeNumber = staffDto.ESICodeNumber,
                ReportingManager = staffDto.ReportingManager,
                Reportee = staffDto.Reportee,
                AadharNumber = staffDto.AadharNumber,
                PANNumber = staffDto.PANNumber,
                Religion = staffDto.Religion,
                Category = staffDto.Category,
                FatherName = staffDto.FatherName,
                MotherName = staffDto.MotherName,
                MaritalStatus = staffDto.MaritalStatus,
                SpouseName = staffDto.SpouseName,
                EmergencyContactNumber = staffDto.EmergencyContactNumber,
                BankName = staffDto.BankName,
                BankAccountNumber = staffDto.BankAccountNumber,
                IFSCCode = staffDto.IFSCCode,
                AccountHolderName = staffDto.AccountHolderName,
                ProfilePhotoUrl = staffDto.ProfilePhotoUrl
            };

            if (staffDto.PreviousEmployments != null)
            {
                foreach (var prev in staffDto.PreviousEmployments)
                {
                    staff.PreviousEmployments.Add(new EmployeePreviousEmployment
                    {
                        CompanyName = prev.CompanyName,
                        JobTitle = prev.JobTitle,
                        JoiningDate = prev.JoiningDate,
                        RelievingDate = prev.RelievingDate,
                        Location = prev.Location,
                        ReferenceName = prev.ReferenceName,
                        ReferenceMobile = prev.ReferenceMobile
                    });
                }
            }

            _context.Employees.Add(staff);
            await _context.SaveChangesAsync();
            return staff;
        }
        private string GenerateEmployeeId(string rolePrefix = "TCH")
        {
            // Optional: Include year
            string yearPart = DateTime.Now.Year.ToString().Substring(2, 2); // e.g., "23"
            string prefix = $"{rolePrefix}-{yearPart}-";

            // Get last employee with same prefix
            var lastStaff = _context.Employees
                .Where(s => s.EmployeeId.StartsWith(prefix))
                .OrderByDescending(s => s.EmployeeId)
                .FirstOrDefault();

            int nextNumber = 1;
            if (lastStaff != null)
            {
                var numberPart = lastStaff.EmployeeId.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"{prefix}{nextNumber:D4}"; // e.g., STF-23-0001
        }

        public async Task<EmployeeModel?> GetStaffByIdAsync(Guid id, Guid? SchoolId)
        {
            return await _context.Employees
                .Include(s => s.PreviousEmployments)
                .FirstOrDefaultAsync(s => s.Id == id && s.SchoolId == SchoolId);
        }

        public async Task<List<EmployeeModel>> GetAllStaffAsync(Guid? SchoolId)
        {
            return await _context.Employees
                .Include(s => s.PreviousEmployments)
                .Where(x => x.SchoolId == SchoolId)
                .ToListAsync();
        }

        public async Task<EmployeeModel?> UpdateStaffAsync(Guid id, EmployeeDto staffDto, Guid? SchoolId)
        {
            var staff = await _context.Employees
                .Include(s => s.PreviousEmployments)
                .FirstOrDefaultAsync(s => s.Id == id && s.SchoolId == SchoolId);
            if (staff == null) return null;

            staff.FirstName = staffDto.FirstName;
            staff.MiddleName = staffDto.MiddleName;
            staff.LastName = staffDto.LastName;
            staff.Email = staffDto.Email;
            staff.MobileNumber = staffDto.MobileNumber;
            staff.UserRole = staffDto.UserRole;
            staff.Department = staffDto.Department;
            staff.ProfilePhotoUrl = staffDto.ProfilePhotoUrl;

            // Replace previous employments
            staff.PreviousEmployments.Clear();
            if (staffDto.PreviousEmployments != null)
            {
                foreach (var prev in staffDto.PreviousEmployments)
                {
                    staff.PreviousEmployments.Add(new EmployeePreviousEmployment
                    {
                        CompanyName = prev.CompanyName,
                        JobTitle = prev.JobTitle,
                        JoiningDate = prev.JoiningDate,
                        RelievingDate = prev.RelievingDate,
                        Location = prev.Location,
                        ReferenceName = prev.ReferenceName,
                        ReferenceMobile = prev.ReferenceMobile
                    });
                }
            }

            await _context.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> DeleteStaffAsync(Guid id, Guid? SchoolId)
        {
            var staff = await _context.Employees
                .Include(s => s.PreviousEmployments)
                .FirstOrDefaultAsync(s => s.Id == id && s.SchoolId == SchoolId);
            if (staff == null) return false;

            _context.Employees.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
