using Darwin.Membership.API.Models.Plan;
using Darwin.Membership.API.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Membership.API.Controllers;

public class PlanController : CustomBaseController
{
    private readonly IPlanService _planService;

    public PlanController(IPlanService planService)
    {
        _planService = planService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlans()
    {
        var plans = await _planService.GetAllAsync();
        return CreateActionResult(plans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var plan = await _planService.GetByIdAsync(id);
        return CreateActionResult(plan);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanRequest plan)
    {
        var createdPlan = await _planService.CreateAsync(plan);
        return CreateActionResult(createdPlan);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlan(Guid id, [FromBody] UpdatePlanRequest plan)
    {
        var updatedPlan = await _planService.UpdateAsync(id, plan);
        return CreateActionResult(updatedPlan);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlan(Guid id)
    {
        var deletePlanResponse=await _planService.DeleteAsync(id);
        return CreateActionResult(deletePlanResponse);
    }
}
