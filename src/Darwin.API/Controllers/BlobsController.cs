using Darwin.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BlobsController : ControllerBase
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public BlobsController(IAzureBlobStorageService azureBlobStorageService)
        {
            _azureBlobStorageService = azureBlobStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> ListBlobs(string containerName)
        {

            return Ok(await _azureBlobStorageService.ListAsync(containerName));
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,string containerName)
        {  
            var response= await _azureBlobStorageService.UploadAsync(file,containerName);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Download(string fileName,string containerName)
        {
            var response= await _azureBlobStorageService.DownloadAsync(fileName,containerName);
            return File(response.Content,response.ContentType,response.Name);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string fileName,string containerName)
        {
            return Ok( await _azureBlobStorageService.DeleteAsync(fileName, containerName));
        }
    }
}
