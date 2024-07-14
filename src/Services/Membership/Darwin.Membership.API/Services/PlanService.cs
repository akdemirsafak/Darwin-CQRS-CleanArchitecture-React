using AutoMapper;
using Darwin.Membership.API.Entities;
using Darwin.Membership.API.Models.Plan;
using Darwin.Membership.API.Repositories;
using Darwin.Shared.Dtos;

namespace Darwin.Membership.API.Services;

public class PlanService : IPlanService
{
    private readonly IPlanRepository _planRepository;
    private readonly IMapper _mapper;

    public PlanService(
        IPlanRepository planRepository,
        IMapper mapper)
    {
        _planRepository = planRepository;
        _mapper = mapper;
    }

    public async Task<DarwinResponse<GetPlanResponse>> CreateAsync(CreatePlanRequest request)
    {

        var plan = _mapper.Map<Plan>(request);

        await _planRepository.CreateAsync(plan);

        return DarwinResponse<GetPlanResponse>.Success(_mapper.Map<GetPlanResponse>(plan), 201);
    }

    public async Task<DarwinResponse<bool>> DeleteAsync(Guid id)
    {
        var entity = await _planRepository.GetByIdAsync(id);
        if (entity == null)
        {
            return DarwinResponse<bool>.Fail("Plan not found");
        }
        await _planRepository.DeleteAsync(entity);
        return DarwinResponse<bool>.Success(true);
    }

    public async Task<DarwinResponse<List<GetPlanResponse>>> GetAllAsync()
    {
        var entities = await _planRepository.GetAllAsync();
        return DarwinResponse<List<GetPlanResponse>>.Success(_mapper.Map<List<GetPlanResponse>>(entities));
    }

    public async Task<DarwinResponse<GetPlanResponse>> GetByIdAsync(Guid id)
    {
        var entity = await _planRepository.GetByIdAsync(id);
        if (entity == null)
            return DarwinResponse<GetPlanResponse>.Fail("Plan not found");

        return DarwinResponse<GetPlanResponse>.Success(_mapper.Map<GetPlanResponse>(entity));
    }

    public async Task<DarwinResponse<GetPlanResponse>> UpdateAsync(Guid id, UpdatePlanRequest request)
    {

        var entity = await _planRepository.GetByIdAsync(id);
        if (entity == null)
            return DarwinResponse<GetPlanResponse>.Fail("Plan not found");

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.IsActive = request.IsActive;
        entity.UpdatedAt = DateTime.Now;

        await _planRepository.UpdateAsync(entity);

        return DarwinResponse<GetPlanResponse>.Success(_mapper.Map<GetPlanResponse>(entity));
    }
}
