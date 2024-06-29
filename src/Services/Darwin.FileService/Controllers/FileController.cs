using Darwin.FileService.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.FileService.Controllers;

public class FileController : CustomBaseController
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
