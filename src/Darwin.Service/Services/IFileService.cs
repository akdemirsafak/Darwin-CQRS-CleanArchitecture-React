using Microsoft.AspNetCore.Http;

namespace Darwin.Service.Services;

public interface IFileService
{
    Task<string> UploadImage(IFormFile image);
}
