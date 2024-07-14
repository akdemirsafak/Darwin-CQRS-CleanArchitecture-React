using AutoMapper;
using Darwin.Membership.API.Entities;
using Darwin.Membership.API.Models.Plan;

namespace Darwin.Membership.API.Mappers;
public class PlanMapper : Profile
{
    public PlanMapper()
    {
        CreateMap<CreatePlanRequest, Plan>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Plan, GetPlanResponse>();
    }
}