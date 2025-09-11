using AutoMapper;
using SchoolNexAPI.DTOs.Fee;
using SchoolNexAPI.Models.Fees;

namespace SchoolNexAPI.Mapping
{
    public class FeeMappingProfile : Profile
    {
        public FeeMappingProfile()
        {
            CreateMap<FeeStructure, FeeStructureDto>();
            CreateMap<FeeStructureItem, FeeStructureItemDto>();

            CreateMap<StudentFeePlan, StudentFeePlanDto>()
                .ForMember(d => d.Lines, o => o.MapFrom(s => s.Lines));
            CreateMap<StudentFeePlanLine, StudentFeePlanLineDto>();

            CreateMap<Invoice, InvoiceDto>()
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.TotalAmount))
                .ForMember(d => d.AmountDue, o => o.MapFrom(s => s.AmountDue));
            CreateMap<InvoiceLine, InvoiceLineDto>();

            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<Adjustment, AdjustmentResponseDto>().ForMember(d => d.Type, o => o.MapFrom(s => s.Type.ToString()));
        }
    }
}
