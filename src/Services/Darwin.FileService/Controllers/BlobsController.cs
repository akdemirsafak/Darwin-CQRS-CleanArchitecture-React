using Darwin.FileService.Services;
using Darwin.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.FileService.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class BlobsController : CustomBaseController
{
    private readonly IAzureBlobStorageService _azureBlobStorageService;

    public BlobsController(IAzureBlobStorageService azureBlobStorageService)
    {
        _azureBlobStorageService = azureBlobStorageService;
    }

    [HttpGet]
    public async Task<IActionResult> ListBlobs(string containerName)
    {
        return CreateActionResult(await _azureBlobStorageService.ListAsync(containerName));
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string containerName)
    {
        var response= await _azureBlobStorageService.UploadAsync(file,containerName);
        return CreateActionResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string fileName, string containerName)
    {
        return CreateActionResult(await _azureBlobStorageService.DeleteAsync(fileName, containerName));
    }
}