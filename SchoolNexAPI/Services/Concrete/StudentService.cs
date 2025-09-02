using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SchoolNexAPI.Data;
using SchoolNexAPI.DTOs;
using SchoolNexAPI.Enums;
using SchoolNexAPI.Extensions;
using SchoolNexAPI.Helpers;
using SchoolNexAPI.Migrations;
using SchoolNexAPI.Models;
using SchoolNexAPI.Models.Student;
using SchoolNexAPI.Services.Abstract;
using System.Linq;
using System.Net;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Collections.Specialized.BitVector32;

namespace SchoolNexAPI.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IAzureService _azureService;
        private readonly UserHelper _userHelper;

        public StudentService(AppDbContext context, IAzureService azureService, UserHelper userHelper)
        {
            _context = context;
            this._azureService = azureService;
            this._userHelper = userHelper;
        }

        public async Task<StudentDto> CreateAsync(Guid schoolId, CreateStudentRequest request, string createdBy)
        {
            var student = new StudentModel
            {
                SchoolId = schoolId,
                FullName = request.FullName,
                Class = request.Class,
                Section = request.Section,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                AcademicYearId = request.AcademicYearId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            if (request.Photo != null)
            {
                student.PhotoPath = await _azureService.UploadFileAsync(
                    request.Photo,
                    BlobEntity.Students,
                    student.Id.ToString(),
                    BlobCategory.ProfilePictures,
                    student.SchoolId.ToString()
                );

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }

            if (request.Address != null)
            {
                var address = new StudentAddressModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    Line1 = request.Address.Line1,
                    Line2 = request.Address.Line2,
                    City = request.Address.City,
                    State = request.Address.State,
                    PinCode = request.Address.PinCode,
                    Country = request.Address.Country,
                    IsPermanentSameAsCurrent = request.Address.IsPermanentSameAsCurrent ?? false
                };
                _context.StudentAddresses.Add(address);
            }

            if (request.Parents != null && request.Parents.Any())
            {
                foreach (var parent in request.Parents)
                {
                    var parentEntity = new StudentParentModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        Name = parent.Name,
                        Relation = parent.Relation,
                        PhoneNumber = parent.PhoneNumber,
                        Email = parent.Email,
                        Education = parent.Education,
                        Occupation = parent.Occupation,
                        Organization = parent.Organization,
                        Designation = parent.Designation,
                        AnnualIncome = parent.AnnualIncome,
                        OfficeNumber = parent.OfficeNumber,
                    };
                    _context.StudentParents.Add(parentEntity);
                }
            }

            if (request.BankDetails != null)
            {
                var bank = new StudentBankDetailsModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    BankName = request.BankDetails.BankName,
                    AccountNumber = request.BankDetails.AccountNumber,
                    IFSC = request.BankDetails.IFSC,
                    AccountHolderName = request.BankDetails.AccountHolderName,
                };
                _context.StudentBanks.Add(bank);
            }

            if (request.MedicalRecord != null)
            {
                var medical = new StudentMedicalRecordModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    Weight = request.MedicalRecord.Weight,
                    Height = request.MedicalRecord.Height,
                    BMI = request.MedicalRecord.BMI,
                    PulseRate = request.MedicalRecord.PulseRate,
                    Haemoglobin = request.MedicalRecord.Haemoglobin,
                    Allergies = request.MedicalRecord.Allergies,
                    COVIDVaccination = request.MedicalRecord.COVIDVaccination,
                    ChildImmunisation = request.MedicalRecord.ChildImmunisation,
                    ImmunisationRemarks = request.MedicalRecord.ImmunisationRemarks,
                };
                _context.StudentMedicalRecords.Add(medical);
            }

            if (request.PreviousSchool != null)
            {
                var previous = new StudentPreviousSchoolModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    Name = request.PreviousSchool.Name,
                    Address = request.PreviousSchool.Address,
                    Board = request.PreviousSchool.Board,
                    MediumOfInstruction = request.PreviousSchool.MediumOfInstruction,
                    TCNumber = request.PreviousSchool.TCNumber,
                    LastClassPassed = request.PreviousSchool.LastClassPassed,
                    PercentageOrGrade = request.PreviousSchool.PercentageOrGrade,
                };
                _context.StudentPreviousSchools.Add(previous);
            }

            if (request.Transportation != null)
            {
                var transport = new StudentTransportationModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    NeedsTransportation = request.Transportation.NeedsTransportation,
                    PickupPoint = request.Transportation.PickupPoint,
                };
                _context.StudentTransportations.Add(transport);
            }

            if (request.AdditionalDetails != null)
            {
                var additionalDetails = new StudentAdditionalDetailsModel
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    SchoolId = schoolId,
                    Religion = request.AdditionalDetails.Religion,
                    Category = request.AdditionalDetails.Category,
                    RightToEducation = request.AdditionalDetails.RightToEducation,
                    Nationality = request.AdditionalDetails.Nationality,
                    BPLStudent = request.AdditionalDetails.BPLStudent,
                    BPLCardNo = request.AdditionalDetails.BPLCardNo,
                    PwD = request.AdditionalDetails.PwD,
                    TypeOfDisability = request.AdditionalDetails.TypeOfDisability,
                    IdentificationMark = request.AdditionalDetails.IdentificationMark,
                    MotherTongue = request.AdditionalDetails.MotherTongue,
                    SecondLanguage = request.AdditionalDetails.SecondLanguage,
                    EmergencyContactNumber = request.AdditionalDetails.EmergencyContactNumber,
                    SingleParentChild = request.AdditionalDetails.SingleParentChild,
                    SingleParent = request.AdditionalDetails.SingleParent,
                    SponsoredStudent = request.AdditionalDetails.SponsoredStudent,
                    SponsorName = request.AdditionalDetails.SponsorName
                };
                _context.StudentAdditionalDetails.Add(additionalDetails);

                await _context.SaveChangesAsync();
            }

            return await GetByIdAsync(student.Id);
        }
        public async Task<StudentDto> GetByIdAsync(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.Parents)
                .Include(s => s.BankDetails)
                .Include(s => s.MedicalRecord)
                .Include(s => s.PreviousSchool)
                .Include(s => s.Transportation)
                .Include(s => s.AdditionalDetails)
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return null;

            var studentData = new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Class = student.Class,
                Section = student.Section,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                IsActive = student.IsActive,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                PhotoUrl = await _azureService.GetSasUrlAsync(student.PhotoPath),
                AcademicYearId = student.AcademicYearId,
                CreatedAt = student.CreatedAt,
                UpdatedAt = student.UpdatedAt,
                CreatedBy = student.CreatedBy,
                UpdatedBy = student.UpdatedBy,


                Address = new AddressDto
                {
                    Line1 = student.Address.Line1,
                    Line2 = student.Address.Line2,
                    City = student.Address.City,
                    State = student.Address.State,
                    PinCode = student.Address.PinCode,
                    Country = student.Address.Country,
                    IsPermanentSameAsCurrent = student.Address.IsPermanentSameAsCurrent
                },
                Parents = student.Parents?.Select(p => new ParentDto
                {
                    Name = p.Name,
                    Relation = p.Relation,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Education = p.Education,
                    Occupation = p.Occupation,
                    Organization = p.Organization,
                    Designation = p.Designation,
                    AnnualIncome = p.AnnualIncome,
                    OfficeNumber = p.OfficeNumber
                }).ToList(),
                BankDetails = student.BankDetails == null ? null : new BankDetailsDto
                {
                    BankName = student.BankDetails.BankName,
                    AccountNumber = student.BankDetails.AccountNumber,
                    IFSC = student.BankDetails.IFSC,
                    AccountHolderName = student.BankDetails.AccountHolderName
                },
                MedicalRecord = student.MedicalRecord == null ? null : new MedicalRecordDto
                {
                    Weight = student.MedicalRecord.Weight,
                    Height = student.MedicalRecord.Height,
                    BMI = student.MedicalRecord.BMI,
                    PulseRate = student.MedicalRecord.PulseRate,
                    Haemoglobin = student.MedicalRecord.Haemoglobin,
                    Allergies = student.MedicalRecord.Allergies,
                    COVIDVaccination = student.MedicalRecord.COVIDVaccination,
                    ChildImmunisation = student.MedicalRecord.ChildImmunisation,
                    ImmunisationRemarks = student.MedicalRecord.ImmunisationRemarks
                },
                PreviousSchool = student.PreviousSchool == null ? null : new PreviousSchoolDto
                {
                    Name = student.PreviousSchool.Name,
                    Address = student.PreviousSchool.Address,
                    Board = student.PreviousSchool.Board,
                    MediumOfInstruction = student.PreviousSchool.MediumOfInstruction,
                    TCNumber = student.PreviousSchool.TCNumber,
                    LastClassPassed = student.PreviousSchool.LastClassPassed,
                    PercentageOrGrade = student.PreviousSchool.PercentageOrGrade
                },
                Transportation = student.Transportation == null ? null : new TransportationDto
                {
                    NeedsTransportation = student.Transportation.NeedsTransportation,
                    PickupPoint = student.Transportation.PickupPoint
                },
                AdditionalDetails = new AdditionalDetailsDto
                {
                    Religion = student.AdditionalDetails.Religion,
                    Category = student.AdditionalDetails.Category,
                    RightToEducation = student.AdditionalDetails.RightToEducation,
                    Nationality = student.AdditionalDetails.Nationality,
                    BPLStudent = student.AdditionalDetails.BPLStudent,
                    BPLCardNo = student.AdditionalDetails.BPLCardNo,
                    PwD = student.AdditionalDetails.PwD,
                    TypeOfDisability = student.AdditionalDetails.TypeOfDisability,
                    IdentificationMark = student.AdditionalDetails.IdentificationMark,
                    MotherTongue = student.AdditionalDetails.MotherTongue,
                    SecondLanguage = student.AdditionalDetails.SecondLanguage,
                    EmergencyContactNumber = student.AdditionalDetails.EmergencyContactNumber,
                    SingleParentChild = student.AdditionalDetails.SingleParentChild,
                    SingleParent = student.AdditionalDetails.SingleParent,
                    SponsoredStudent = student.AdditionalDetails.SponsoredStudent,
                    SponsorName = student.AdditionalDetails.SponsorName
                }
            };

            return studentData;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(Guid schoolId, string status = "all")
        {
            IQueryable<StudentModel> query = _context.Students
                .Include(s => s.Parents)
                .Include(s => s.Address)
                .Include(s => s.BankDetails)
                .Include(s => s.MedicalRecord)
                .Include(s => s.PreviousSchool)
                .Include(s => s.Transportation)
                .Include(s => s.AdditionalDetails);

            query = query.FilterBySchool(schoolId, _userHelper.IsSuperAdmin());

            switch (status.ToLower())
            {
                case "active":
                    query = query.Where(s => s.IsActive);
                    break;
                case "inactive":
                    query = query.Where(s => !s.IsActive);
                    break;
            }

            var students = await query.ToListAsync();

            var studentDtos = await Task.WhenAll(students.Select(async s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Class = s.Class,
                Section = s.Section,
                DateOfBirth = s.DateOfBirth,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                IsActive = s.IsActive,
                Gender = s.Gender,
                AcademicYearId = s.AcademicYearId,
                PhotoUrl = await _azureService.GetSasUrlAsync(s.PhotoPath), // assuming you store photo URL

                Address = s.Address != null ? new AddressDto
                {
                    Line1 = s.Address.Line1,
                    Line2 = s.Address.Line2,
                    City = s.Address.City,
                    State = s.Address.State,
                    PinCode = s.Address.PinCode,
                    Country = s.Address.Country,
                    IsPermanentSameAsCurrent = s.Address.IsPermanentSameAsCurrent
                } : null,

                Parents = s.Parents?.Select(p => new ParentDto
                {
                    Name = p.Name,
                    Relation = p.Relation,
                    PhoneNumber = p.PhoneNumber,
                    Email = p.Email,
                    Education = p.Education,
                    Occupation = p.Occupation,
                    Organization = p.Organization,
                    Designation = p.Designation,
                    AnnualIncome = p.AnnualIncome,
                    OfficeNumber = p.OfficeNumber
                }).ToList(),

                BankDetails = s.BankDetails != null ? new BankDetailsDto
                {
                    BankName = s.BankDetails.BankName,
                    AccountNumber = s.BankDetails.AccountNumber,
                    IFSC = s.BankDetails.IFSC,
                    AccountHolderName = s.BankDetails.AccountHolderName
                } : null,

                MedicalRecord = s.MedicalRecord != null ? new MedicalRecordDto
                {
                    Weight = s.MedicalRecord.Weight,
                    Height = s.MedicalRecord.Height,
                    BMI = s.MedicalRecord.BMI,
                    PulseRate = s.MedicalRecord.PulseRate,
                    Haemoglobin = s.MedicalRecord.Haemoglobin,
                    Allergies = s.MedicalRecord.Allergies,
                    COVIDVaccination = s.MedicalRecord.COVIDVaccination,
                    ChildImmunisation = s.MedicalRecord.ChildImmunisation,
                    ImmunisationRemarks = s.MedicalRecord.ImmunisationRemarks
                } : null,

                PreviousSchool = s.PreviousSchool != null ? new PreviousSchoolDto
                {
                    Name = s.PreviousSchool.Name,
                    Address = s.PreviousSchool.Address,
                    Board = s.PreviousSchool.Board,
                    MediumOfInstruction = s.PreviousSchool.MediumOfInstruction,
                    TCNumber = s.PreviousSchool.TCNumber,
                    LastClassPassed = s.PreviousSchool.LastClassPassed,
                    PercentageOrGrade = s.PreviousSchool.PercentageOrGrade
                } : null,

                Transportation = s.Transportation != null ? new TransportationDto
                {
                    NeedsTransportation = s.Transportation.NeedsTransportation,
                    PickupPoint = s.Transportation.PickupPoint
                } : null,

                AdditionalDetails = s.AdditionalDetails != null ? new AdditionalDetailsDto
                {
                    Religion = s.AdditionalDetails.Religion,
                    Category = s.AdditionalDetails.Category,
                    RightToEducation = s.AdditionalDetails.RightToEducation,
                    Nationality = s.AdditionalDetails.Nationality,
                    BPLStudent = s.AdditionalDetails.BPLStudent,
                    BPLCardNo = s.AdditionalDetails.BPLCardNo,
                    PwD = s.AdditionalDetails.PwD,
                    TypeOfDisability = s.AdditionalDetails.TypeOfDisability,
                    IdentificationMark = s.AdditionalDetails.IdentificationMark,
                    MotherTongue = s.AdditionalDetails.MotherTongue,
                    SecondLanguage = s.AdditionalDetails.SecondLanguage,
                    EmergencyContactNumber = s.AdditionalDetails.EmergencyContactNumber,
                    SingleParentChild = s.AdditionalDetails.SingleParentChild,
                    SingleParent = s.AdditionalDetails.SingleParent,
                    SponsoredStudent = s.AdditionalDetails.SponsoredStudent,
                    SponsorName = s.AdditionalDetails.SponsorName
                } : null,

                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy
            }));

            return studentDtos;
        }

        public async Task<bool> UpdateAsync(Guid studentId, Guid schoolId, UpdateStudentRequest request, string updatedBy)
        {

            var student = await _context.Students
                .Include(s => s.Address)
                .Include(s => s.Parents)
                .Include(s => s.BankDetails)
                .Include(s => s.MedicalRecord)
                .Include(s => s.PreviousSchool)
                .Include(s => s.Transportation)
                .Include(s => s.AdditionalDetails)
                .Where(s => s.Id == studentId && (schoolId == Guid.Empty || s.SchoolId == schoolId))
                .FirstOrDefaultAsync();



            if (student == null)
                throw new KeyNotFoundException("Student not found");

            // Update main student fields
            student.FullName = request.FullName ?? student.FullName;
            student.Class = request.Class ?? student.Class;
            student.Section = request.Section ?? student.Section;
            student.DateOfBirth = request.DateOfBirth ?? student.DateOfBirth;
            student.Email = request.Email ?? student.Email;
            student.PhoneNumber = request.PhoneNumber ?? student.PhoneNumber;
            student.Gender = request.Gender ?? student.Gender;
            student.AcademicYearId = request.AcademicYearId ?? student.AcademicYearId;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = updatedBy;

            // Update photo if provided
            if (request.Photo != null)
            {
                student.PhotoPath = await _azureService.UploadFileAsync(
                    request.Photo,
                    BlobEntity.Students,
                    student.Id.ToString(),
                    BlobCategory.ProfilePictures,
                    student.SchoolId.ToString()
                );
            }

            // Update or create address
            if (request.Address != null)
            {
                if (student.Address != null)
                {
                    student.Address.Line1 = request.Address.Line1 ?? student.Address.Line1;
                    student.Address.Line2 = request.Address.Line2 ?? student.Address.Line2;
                    student.Address.City = request.Address.City ?? student.Address.City;
                    student.Address.State = request.Address.State ?? student.Address.State;
                    student.Address.PinCode = request.Address.PinCode ?? student.Address.PinCode;
                    student.Address.Country = request.Address.Country ?? student.Address.Country;
                    student.Address.IsPermanentSameAsCurrent = request.Address.IsPermanentSameAsCurrent ?? student.Address.IsPermanentSameAsCurrent;
                }
                else
                {
                    var address = new StudentAddressModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        Line1 = request.Address.Line1,
                        Line2 = request.Address.Line2,
                        City = request.Address.City,
                        State = request.Address.State,
                        PinCode = request.Address.PinCode,
                        Country = request.Address.Country,
                        IsPermanentSameAsCurrent = request.Address.IsPermanentSameAsCurrent ?? false
                    };
                    _context.StudentAddresses.Add(address);
                }
            }

            // Update parents
            if (request.Parents != null && request.Parents.Any())
            {
                foreach (var parentRequest in request.Parents)
                {
                    // Try to find existing parent by StudentId + Relation
                    var existingParent = await _context.StudentParents
                        .FirstOrDefaultAsync(p => p.StudentId == student.Id && p.Relation == parentRequest.Relation);

                    if (existingParent != null)
                    {
                        // Update existing parent
                        existingParent.Name = parentRequest.Name ?? existingParent.Name;
                        existingParent.PhoneNumber = parentRequest.PhoneNumber ?? existingParent.PhoneNumber;
                        existingParent.Email = parentRequest.Email ?? existingParent.Email;
                        existingParent.Education = parentRequest.Education ?? existingParent.Education;
                        existingParent.Occupation = parentRequest.Occupation ?? existingParent.Occupation;
                        existingParent.Organization = parentRequest.Organization ?? existingParent.Organization;
                        existingParent.Designation = parentRequest.Designation ?? existingParent.Designation;
                        existingParent.AnnualIncome = parentRequest.AnnualIncome ?? existingParent.AnnualIncome;
                        existingParent.OfficeNumber = parentRequest.OfficeNumber ?? existingParent.OfficeNumber;
                    }
                    else
                    {
                        // Add new parent
                        var newParent = new StudentParentModel
                        {
                            Id = Guid.NewGuid(),
                            StudentId = student.Id,
                            SchoolId = schoolId,
                            Name = parentRequest.Name,
                            Relation = parentRequest.Relation,
                            PhoneNumber = parentRequest.PhoneNumber,
                            Email = parentRequest.Email,
                            Education = parentRequest.Education,
                            Occupation = parentRequest.Occupation,
                            Organization = parentRequest.Organization,
                            Designation = parentRequest.Designation,
                            AnnualIncome = parentRequest.AnnualIncome,
                            OfficeNumber = parentRequest.OfficeNumber,
                        };
                        _context.StudentParents.Add(newParent);
                    }
                }
            }


            // Update bank details
            if (request.BankDetails != null)
            {
                if (student.BankDetails != null)
                {
                    student.BankDetails.BankName = request.BankDetails.BankName ?? student.BankDetails.BankName;
                    student.BankDetails.AccountNumber = request.BankDetails.AccountNumber ?? student.BankDetails.AccountNumber;
                    student.BankDetails.IFSC = request.BankDetails.IFSC ?? student.BankDetails.IFSC;
                    student.BankDetails.AccountHolderName = request.BankDetails.AccountHolderName ?? student.BankDetails.AccountHolderName;
                }
                else
                {
                    var bank = new StudentBankDetailsModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        BankName = request.BankDetails.BankName,
                        AccountNumber = request.BankDetails.AccountNumber,
                        IFSC = request.BankDetails.IFSC,
                        AccountHolderName = request.BankDetails.AccountHolderName,
                    };
                    _context.StudentBanks.Add(bank);
                }
            }

            // Update medical record
            if (request.MedicalRecord != null)
            {
                if (student.MedicalRecord != null)
                {
                    student.MedicalRecord.Weight = request.MedicalRecord.Weight ?? student.MedicalRecord.Weight;
                    student.MedicalRecord.Height = request.MedicalRecord.Height ?? student.MedicalRecord.Height;
                    student.MedicalRecord.BMI = request.MedicalRecord.BMI ?? student.MedicalRecord.BMI;
                    student.MedicalRecord.PulseRate = request.MedicalRecord.PulseRate ?? student.MedicalRecord.PulseRate;
                    student.MedicalRecord.Haemoglobin = request.MedicalRecord.Haemoglobin ?? student.MedicalRecord.Haemoglobin;
                    student.MedicalRecord.Allergies = request.MedicalRecord.Allergies ?? student.MedicalRecord.Allergies;
                    student.MedicalRecord.COVIDVaccination = request.MedicalRecord.COVIDVaccination;
                    student.MedicalRecord.ChildImmunisation = request.MedicalRecord.ChildImmunisation;
                    student.MedicalRecord.ImmunisationRemarks = request.MedicalRecord.ImmunisationRemarks ?? student.MedicalRecord.ImmunisationRemarks;
                }
                else
                {
                    var medical = new StudentMedicalRecordModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        Weight = request.MedicalRecord.Weight,
                        Height = request.MedicalRecord.Height,
                        BMI = request.MedicalRecord.BMI,
                        PulseRate = request.MedicalRecord.PulseRate,
                        Haemoglobin = request.MedicalRecord.Haemoglobin,
                        Allergies = request.MedicalRecord.Allergies,
                        COVIDVaccination = request.MedicalRecord.COVIDVaccination,
                        ChildImmunisation = request.MedicalRecord.ChildImmunisation,
                        ImmunisationRemarks = request.MedicalRecord.ImmunisationRemarks,
                    };
                    _context.StudentMedicalRecords.Add(medical);
                }
            }

            // Update previous school
            if (request.PreviousSchool != null)
            {
                if (student.PreviousSchool != null)
                {
                    student.PreviousSchool.Name = request.PreviousSchool.Name ?? student.PreviousSchool.Name;
                    student.PreviousSchool.Address = request.PreviousSchool.Address ?? student.PreviousSchool.Address;
                    student.PreviousSchool.Board = request.PreviousSchool.Board ?? student.PreviousSchool.Board;
                    student.PreviousSchool.MediumOfInstruction = request.PreviousSchool.MediumOfInstruction ?? student.PreviousSchool.MediumOfInstruction;
                    student.PreviousSchool.TCNumber = request.PreviousSchool.TCNumber ?? student.PreviousSchool.TCNumber;
                    student.PreviousSchool.LastClassPassed = request.PreviousSchool.LastClassPassed ?? student.PreviousSchool.LastClassPassed;
                    student.PreviousSchool.PercentageOrGrade = request.PreviousSchool.PercentageOrGrade ?? student.PreviousSchool.PercentageOrGrade;
                }
                else
                {
                    var previous = new StudentPreviousSchoolModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        Name = request.PreviousSchool.Name,
                        Address = request.PreviousSchool.Address,
                        Board = request.PreviousSchool.Board,
                        MediumOfInstruction = request.PreviousSchool.MediumOfInstruction,
                        TCNumber = request.PreviousSchool.TCNumber,
                        LastClassPassed = request.PreviousSchool.LastClassPassed,
                        PercentageOrGrade = request.PreviousSchool.PercentageOrGrade,
                    };
                    _context.StudentPreviousSchools.Add(previous);
                }
            }

            // Update transportation
            if (request.Transportation != null)
            {
                if (student.Transportation != null)
                {
                    student.Transportation.NeedsTransportation = request.Transportation.NeedsTransportation;
                    student.Transportation.PickupPoint = request.Transportation.PickupPoint ?? student.Transportation.PickupPoint;
                }
                else
                {
                    var transport = new StudentTransportationModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        NeedsTransportation = request.Transportation.NeedsTransportation,
                        PickupPoint = request.Transportation.PickupPoint,
                    };
                    _context.StudentTransportations.Add(transport);
                }
            }

            // Update additional details
            if (request.AdditionalDetails != null)
            {
                if (student.AdditionalDetails != null)
                {
                    student.AdditionalDetails.Religion = request.AdditionalDetails.Religion ?? student.AdditionalDetails.Religion;
                    student.AdditionalDetails.Category = request.AdditionalDetails.Category ?? student.AdditionalDetails.Category;
                    student.AdditionalDetails.RightToEducation = request.AdditionalDetails.RightToEducation;
                    student.AdditionalDetails.Nationality = request.AdditionalDetails.Nationality ?? student.AdditionalDetails.Nationality;
                    student.AdditionalDetails.BPLStudent = request.AdditionalDetails.BPLStudent;
                    student.AdditionalDetails.BPLCardNo = request.AdditionalDetails.BPLCardNo ?? student.AdditionalDetails.BPLCardNo;
                    student.AdditionalDetails.PwD = request.AdditionalDetails.PwD;
                    student.AdditionalDetails.TypeOfDisability = request.AdditionalDetails.TypeOfDisability ?? student.AdditionalDetails.TypeOfDisability;
                    student.AdditionalDetails.IdentificationMark = request.AdditionalDetails.IdentificationMark ?? student.AdditionalDetails.IdentificationMark;
                    student.AdditionalDetails.MotherTongue = request.AdditionalDetails.MotherTongue ?? student.AdditionalDetails.MotherTongue;
                    student.AdditionalDetails.SecondLanguage = request.AdditionalDetails.SecondLanguage ?? student.AdditionalDetails.SecondLanguage;
                    student.AdditionalDetails.EmergencyContactNumber = request.AdditionalDetails.EmergencyContactNumber ?? student.AdditionalDetails.EmergencyContactNumber;
                    student.AdditionalDetails.SingleParentChild = request.AdditionalDetails.SingleParentChild;
                    student.AdditionalDetails.SingleParent = request.AdditionalDetails.SingleParent ?? student.AdditionalDetails.SingleParent;
                    student.AdditionalDetails.SponsoredStudent = request.AdditionalDetails.SponsoredStudent;
                    student.AdditionalDetails.SponsorName = request.AdditionalDetails.SponsorName ?? student.AdditionalDetails.SponsorName;
                }
                else
                {
                    var additionalDetails = new StudentAdditionalDetailsModel
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        SchoolId = schoolId,
                        Religion = request.AdditionalDetails.Religion,
                        Category = request.AdditionalDetails.Category,
                        RightToEducation = request.AdditionalDetails.RightToEducation,
                        Nationality = request.AdditionalDetails.Nationality,
                        BPLStudent = request.AdditionalDetails.BPLStudent,
                        BPLCardNo = request.AdditionalDetails.BPLCardNo,
                        PwD = request.AdditionalDetails.PwD,
                        TypeOfDisability = request.AdditionalDetails.TypeOfDisability,
                        IdentificationMark = request.AdditionalDetails.IdentificationMark,
                        MotherTongue = request.AdditionalDetails.MotherTongue,
                        SecondLanguage = request.AdditionalDetails.SecondLanguage,
                        EmergencyContactNumber = request.AdditionalDetails.EmergencyContactNumber,
                        SingleParentChild = request.AdditionalDetails.SingleParentChild,
                        SingleParent = request.AdditionalDetails.SingleParent,
                        SponsoredStudent = request.AdditionalDetails.SponsoredStudent,
                        SponsorName = request.AdditionalDetails.SponsorName
                    };
                    _context.StudentAdditionalDetails.Add(additionalDetails);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.CustomFieldValuesList)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return false;

            // Delete related custom field values
            if (student.CustomFieldValuesList != null && student.CustomFieldValuesList.Any())
            {
                _context.StudentCustomFieldValues.RemoveRange(student.CustomFieldValuesList);
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<string?> UploadPhotoAsync(Guid studentId, Guid schoolId, IFormFile photo, string updatedBy)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == studentId && (schoolId == Guid.Empty || s.SchoolId == schoolId));

            if (student == null)
                throw new KeyNotFoundException("Student not found");

            // Upload to Azure (or wherever you're storing)
            var photoPath = await _azureService.UploadFileAsync(
                photo,
                BlobEntity.Students,
                student.Id.ToString(),
                BlobCategory.ProfilePictures,
                student.SchoolId.ToString()
            );

            if (!string.IsNullOrEmpty(student.PhotoPath))
            {
                await _azureService.DeleteFileAsync(student.PhotoPath);
            }

            student.PhotoPath = photoPath;
            student.UpdatedAt = DateTime.UtcNow;
            student.UpdatedBy = updatedBy;

            try
            {
                await _context.SaveChangesAsync();
                return await _azureService.GetSasUrlAsync(photoPath);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
