using Darwin.Web.Models.Plans;
using Darwin.Web.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Web.Controllers;

public class PlanController : Controller
{
    private readonly IPlanService _planService;

    public PlanController(IPlanService planService)
    {
        _planService = planService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _planService.GetAllAsync());
    }
    public async Task<IActionResult> Details(Guid id) {

        var plan= await _planService.GetByIdAsync(id);
        return View(plan);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreatePlanDto createPlanDto)
    {
        await _planService.CreateAsync(createPlanDto);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var plan= await _planService.GetByIdAsync(id);
        //maple ve yolla
        var updatePlanModel= plan.Adapt<UpdatePlanDto>();
        return View(updatePlanModel);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UpdatePlanDto updatePlanDto)
    {
        await _planService.UpdateAsync(updatePlanDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        await _planService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

}
