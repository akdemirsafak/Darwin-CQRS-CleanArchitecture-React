using Darwin.FileService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.FileService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        return Ok(await _fileService.UploadImage(image));
    }
}
