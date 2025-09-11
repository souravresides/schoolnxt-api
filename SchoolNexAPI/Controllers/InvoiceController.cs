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
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _svc;
        private readonly IMapper _mapper;
        public InvoiceController(IInvoiceService svc, ITenantContext tenant, ILogger<InvoiceController> logger, IMapper mapper) : base(tenant, logger) { _svc = svc; _mapper = mapper; }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateInvoiceDto dto)
        {
            var schoolId = RequireSchoolId(dto.SchoolId);
            var inv = await _svc.GenerateInvoiceFromPlanAsync(schoolId, dto.StudentId, dto.AcademicYearId, dto.DueDate, _tenant.CurrentUserName);
            return Ok(_mapper.Map<InvoiceDto>(inv));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetForStudent(Guid studentId, [FromQuery] Guid schoolId, [FromQuery] Guid academicYearId)
        {
            var invoices = await _svc.ListByStudentAsync(RequireSchoolId(schoolId), studentId, academicYearId);
            return Ok(_mapper.Map<List<InvoiceDto>>(invoices));
        }

        [HttpPut("{invoiceId}/recompute")]
        public async Task<IActionResult> Recompute(Guid invoiceId)
        {
            await _svc.RecomputeInvoiceAsync(invoiceId);
            return NoContent();
        }

    }
}
