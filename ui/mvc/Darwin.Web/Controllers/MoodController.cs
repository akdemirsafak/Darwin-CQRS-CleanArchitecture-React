using Darwin.Web.Models.Moods;
using Darwin.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Web.Controllers
{

    public class MoodController : Controller
    {
        private readonly IMoodService _moodService;

        public MoodController(IMoodService moodService)
        {
            _moodService = moodService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _moodService.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMoodDto createMoodDto)
        {
            await _moodService.CreateAsync(createMoodDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _moodService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}