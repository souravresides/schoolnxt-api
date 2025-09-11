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
    public class FeeStructureController : BaseController
    {
        private readonly IFeeStructureService _svc;
        private readonly IMapper _mapper;
        public FeeStructureController(IFeeStructureService svc, ITenantContext tenant, ILogger<FeeStructureController> logger, IMapper mapper) : base(tenant, logger) { _svc = svc; _mapper = mapper; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeeStructureCreateDto dto)
        {
            var schoolId = RequireSchoolId(dto.SchoolId);
            var fs = await _svc.CreateAsync(dto, _tenant.CurrentUserName, schoolId);
            return Ok(_mapper.Map<FeeStructureDto>(fs));
        }

        [HttpGet("{academicYearId}/{className}")]
        public async Task<IActionResult> GetByClass(Guid academicYearId, string className, [FromQuery] Guid schoolId)
        {
            var list = await _svc.ListByClassAsync(RequireSchoolId(schoolId), academicYearId, className);
            return Ok(_mapper.Map<List<FeeStructureDto>>(list));
        }

    }
}
