using Darwin.Web.Models.Contents;
using Darwin.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Web.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }
        //[HttpGet("contentindex")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contentService.GetAllAsync());
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var content = await _contentService.GetByIdAsync(id);
            return View(content);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateContentDto createContentDto)
        {
            await _contentService.CreateAsync(createContentDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var content = await _contentService.GetByIdAsync(id);
            return View(content);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateContentDto updateContentDto)
        {
            await _contentService.UpdateAsync(updateContentDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }

}