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
    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _svc;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentService svc, ITenantContext tenant, ILogger<PaymentsController> logger, IMapper mapper) : base(tenant, logger) { _svc = svc; _mapper = mapper; }

        [HttpPost]
        public async Task<IActionResult> Record([FromBody] CreatePaymentDto dto, [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
        {
            if (string.IsNullOrWhiteSpace(idempotencyKey)) return BadRequest("Idempotency-Key header required.");
            var schoolId = RequireSchoolId(dto.SchoolId);
            dto.SchoolId = schoolId; // ensure dto uses effective schoolId
            var payment = await _svc.RecordPaymentAsync(dto, _tenant.CurrentUserName, idempotencyKey);
            return Ok(_mapper.Map<PaymentResponseDto>(payment));
        }
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetForStudent(Guid studentId, [FromQuery] Guid schoolId)
        {
            var payments = await _svc.ListByStudentAsync(RequireSchoolId(schoolId), studentId);
            return Ok(_mapper.Map<List<PaymentResponseDto>>(payments));
        }

    }
}
