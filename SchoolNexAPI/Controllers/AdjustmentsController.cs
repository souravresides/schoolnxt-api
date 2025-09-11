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
    public class AdjustmentsController : BaseController
    {
        private readonly IAdjustmentService _svc;
        private readonly IMapper _mapper;
        public AdjustmentsController(IAdjustmentService svc, ITenantContext tenant, ILogger<AdjustmentsController> logger, IMapper mapper) : base(tenant, logger) { _svc = svc; _mapper = mapper; }

        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] CreateAdjustmentDto dto)
        {
            var schoolId = RequireSchoolId(dto.SchoolId);
            dto.SchoolId = schoolId;
            var adj = await _svc.ApplyAsync(dto, _tenant.CurrentUserName);
            return Ok(_mapper.Map<AdjustmentResponseDto>(adj));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetForStudent(Guid studentId, [FromQuery] Guid schoolId)
        {
            var list = await _svc.GetByStudentAsync(RequireSchoolId(schoolId), studentId);
            return Ok(_mapper.Map<List<AdjustmentResponseDto>>(list));
        }

    }
}
