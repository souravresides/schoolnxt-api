using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Services.Abstract;
using SchoolNexAPI.Services.Abstract.Fee;

namespace SchoolNexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFeePlanController : BaseController
    {
        private readonly IStudentFeePlanService _svc;
        private readonly IMapper _mapper;
        public StudentFeePlanController(IStudentFeePlanService svc, ITenantContext tenant, ILogger<StudentFeePlanController> logger, IMapper mapper) : base(tenant, logger) { _svc = svc; _mapper = mapper; }

        [HttpPost]
        public async Task<IActionResult> Generate(Guid studentId, [FromBody] StudentFeePlanCreateDto dto)
        {
            var schoolId = RequireSchoolId(dto.SchoolId);
            var plan = await _svc.GenerateOrRefreshAsync(schoolId, dto.StudentId, dto.AcademicYearId, _tenant.CurrentUserName);
            return Ok(_mapper.Map<StudentFeePlanDto>(plan));
        }
        [HttpGet("{studentId}")]
        public async Task<IActionResult> Get(Guid studentId, [FromQuery] Guid schoolId, [FromQuery] Guid academicYearId)
        {
            var plan = await _svc.GetAsync(RequireSchoolId(schoolId), studentId, academicYearId);
            return Ok(_mapper.Map<StudentFeePlanDto>(plan));
        }

    }
}
